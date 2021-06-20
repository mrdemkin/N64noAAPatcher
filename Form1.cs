using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RomConverter;

namespace N64noAAPatcher
{
    public partial class Form1 : Form
    {
        bool isStarted, needDoJob;
        string lastAddFilePath;
        PatchMachine pMachine;
        public Form1()
        {
            if (!Init())
            {
                Application.Exit();
            }
        }

        private bool Init()
        {
            //init this in form? Great idea!
            //check additionals
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "additionals\\u64aap.exe")))
            {
                MessageBox.Show($"u64aap not found at path: {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "additionals\\u64aap.exe")}");
                return false;
            }
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "additionals\\rn64crc.exe")))
            {
                MessageBox.Show($"rn64crc not found at path: {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "additionals\\rn64crc.exe")}");
                return false;
            }
            this.needDoJob = true;
            this.isStarted = false;
            this.lastAddFilePath = string.Empty;
            InitializeComponent();
            //generate list
            pMachine = new PatchMachine();
            pMachine.OnFilesListChanged += FillTableFiles;
            pMachine.OnFilesListChanged += SetActiveStartBtn;
            SetActiveStartBtn();
            return true;
        }

        private void addFileBtn_Click(object sender, EventArgs e)
        {
            lastAddFilePath = chooseFileToAddDialog();
            if (string.IsNullOrEmpty(lastAddFilePath))
            {
                lastAddFilePath = string.Empty; return;
            }
            pMachine.AddFileToPatch(lastAddFilePath);
        }

        private void chooseOutputBtn_Click(object sender, EventArgs e)
        {
            string nextPath = chooseOutputPathDialog();
            if (string.IsNullOrEmpty(nextPath)) return;
            outputPath.Text = nextPath;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (pMachine.GetFilesToPatch().Count > 0) StartJob();
            else
            {
                MessageBox.Show("Please add rom files first.");
            }
        }

        private string chooseFileToAddDialog()
        {
            string filePath = string.Empty;
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.InitialDirectory = string.IsNullOrEmpty(lastAddFilePath) ? "c:\\" : lastAddFilePath;
                openDialog.Filter = "n64 files (*z64)|*.z64|All files (*.*)|*.*";
                openDialog.FilterIndex = 2;
                openDialog.RestoreDirectory = true;

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openDialog.FileName;

                    //for future inhouse path
                    /*var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }*/
                }
            }
            return filePath;
        }

        private string chooseOutputPathDialog()
        {

            string outputPath = string.Empty;
            using (var openDialog = new FolderBrowserDialog())
            {
                DialogResult result = openDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openDialog.SelectedPath))
                {
                    outputPath = openDialog.SelectedPath;
                }
            }
            return outputPath;
        }

        private void FillTableFiles()
        {
            filesListView.Items.Clear();
            //make it global variable
            ListViewItem[] lItems = new ListViewItem[pMachine.GetFilesToPatch().Count];
            for (int i = 0; i < pMachine.GetFilesToPatch().Count; i++)
            {
                lItems[i] = new ListViewItem(pMachine.GetFilesToPatch().ElementAt(i));
            }

            filesListView.Items.AddRange(lItems);
            /*if (filesListView.Items.Count > 0)
            {
                UnlockRemoveUI();
            }
            else
            {
                LockRemoveUI();
            }*/
        }

        private void SetActiveStartBtn()
        {
            if (pMachine == null) { this.Start.Enabled = false; return; }
            this.Start.Enabled = pMachine.GetFilesToPatch().Count > 0;
        }

        short progressCounter;
        private void StartJob()
        {
            if (isStarted)
            {
                this.isStarted = !this.isStarted;
                needDoJob = false;
                //TODO: STOP PROCESS
            }
            else
            {
                if (pMachine.GetFilesToPatch().Count == 0)
                {
                    MessageBox.Show("Files to patch is empty! Add at least one file.");
                    return;
                }
                if (string.IsNullOrEmpty(outputPath.Text))
                {
                    MessageBox.Show("Output path is incorrect or empty.");
                    return;
                }
                progressCounter = (short)pMachine.GetFilesToPatch().Count;
                mainProgressBar.Maximum = progressCounter;
                mainProgressBar.Minimum = 0;
                mainProgressBar.Value = 0;
                this.isStarted = !this.isStarted;
                LockUI();
                N64 converter = new N64();
                string baseTempPath = outputPath.Text;
                string newOutputPath = string.Empty;
                while (pMachine.GetFilesToPatch().Count > 0 && needDoJob)
                {
                    if (!pMachine.isRunning)
                    {
                        RemoveLastFile(newOutputPath);
                        string path = pMachine.GetNext();
                        newOutputPath = Path.Combine(baseTempPath, $"{Path.GetFileNameWithoutExtension(path)}_temp.z64");
                        converter.Convert(path, newOutputPath);
                        pMachine.StartCmdProcess(CmdAppType.Patcher, $"-i \"{newOutputPath}\" -o \"{this.outputPath.Text}\\{Path.GetFileNameWithoutExtension(path)}_noAA.z64\"");
                        pMachine.StartCmdProcess(CmdAppType.CRC, $"\"{this.outputPath.Text}\\{Path.GetFileNameWithoutExtension(path)}_noAA.z64\"");
                        //pMachine.StartCmdProcess($"--df-off -i \"{newOutputPath}\" -o \"{this.outputPath.Text}\\{Path.GetFileNameWithoutExtension(path)}_noAA.z64\"");
                    }
                    mainProgressBar.Value = progressCounter - pMachine.GetFilesToPatch().Count;
                    FillTableFiles();
                }
                RemoveLastFile(newOutputPath);
                SoftResetAppState();
            }
            /*foreach (var path in pMachine.GetFilesToPatch())
            {
                pMachine.StartCmdProcess($"--df-off -i {path} -o {this.outputPath.Text}\\{Path.GetFileNameWithoutExtension(path)}_noAA.z64");
            }*/
        }

        private void RemoveLastFile(string filePAth)
        {
            if (File.Exists(filePAth))
            {
                File.Delete(filePAth);
            }
        }

        private void RemoveFromList(object sender, EventArgs e)
        {
            if (pMachine.GetFilesToPatch().Count == 0) return;
            foreach (var lview in filesListView.SelectedIndices)
            {
                pMachine.RemoveFileFromPatch((int)lview);
            }
        }

        private void LockUI()
        {
            AddDirBtn.Enabled = false;
            outputPath.Enabled = false;
            addFileBtn.Enabled = false;
            chooseOutputBtn.Enabled = false;
            RemoveFromListBtn.Enabled = false;
            Start.Text = "Stop";
        }


        private void UnlockUI()
        {
            AddDirBtn.Enabled = true;
            outputPath.Enabled = true;
            addFileBtn.Enabled = true;
            chooseOutputBtn.Enabled = true;
            RemoveFromListBtn.Enabled = true;
            Start.Text = "Start";
            needDoJob = true;
        }

        private void LockRemoveUI()
        {
            RemoveFromListBtn.Enabled = false;
        }

        public void UnlockRemoveUI()
        {
            RemoveFromListBtn.Enabled = true;
        }

        private void SoftResetAppState()
        {
            this.isStarted = false;
            needDoJob = true;
            mainProgressBar.Value = 0;
            UnlockUI();
        }

        private void HardResetAppState()
        {
            //SoftResetAppState();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddDirBtn_Click(object sender, EventArgs e)
        {
            string pathForSearch = chooseOutputPathDialog();
            //TODO: make this with search pattern
            var tempArray = Directory.GetFiles(pathForSearch, "*.z64", SearchOption.AllDirectories);
            List<string> filesPathes = new List<string>();
            filesPathes.AddRange(tempArray);
            tempArray = Directory.GetFiles(pathForSearch, "*.v64", SearchOption.AllDirectories);
            filesPathes.AddRange(tempArray);
            tempArray = Directory.GetFiles(pathForSearch, "*.n64", SearchOption.AllDirectories);
            filesPathes.AddRange(tempArray);

            foreach (var path in filesPathes)
            {
                pMachine.AddFileToPatch(path);
            }
            filesPathes.Clear();
        }
    }
}
