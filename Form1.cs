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

namespace N64noAAPatcher
{
    public partial class Form1 : Form
    {
        bool isStarted;
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
        }

        private void SetActiveStartBtn()
        {
            if (pMachine == null) { this.Start.Enabled = false; return; }
            this.Start.Enabled = pMachine.GetFilesToPatch().Count > 0;
        }

        private void StartJob()
        {
            if (!isStarted)
            {
                this.isStarted = !this.isStarted;
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
                this.isStarted = !this.isStarted;
                LockUI();
                while (pMachine.GetFilesToPatch().Count > 0)
                {
                    if (!pMachine.isRunning)
                    {
                        string path = pMachine.GetNext();
                        pMachine.StartCmdProcess($"--df-off -i {path} -o {this.outputPath.Text}\\{Path.GetFileNameWithoutExtension(path)}_noAA.z64");
                    }
                }
                UnlockUI();
            }
            /*foreach (var path in pMachine.GetFilesToPatch())
            {
                pMachine.StartCmdProcess($"--df-off -i {path} -o {this.outputPath.Text}\\{Path.GetFileNameWithoutExtension(path)}_noAA.z64");
            }*/
        }

        private void RemoveFromList(object sender, EventArgs e)
        {
            if (pMachine.GetFilesToPatch().Count == 0) return;
            foreach (ListView.SelectedIndexCollection lview in filesListView.SelectedIndices)
            {
               // lview.
            }
        }

        private void LockUI()
        {
            outputPath.Enabled = false;
            addFileBtn.Enabled = false;
            chooseOutputBtn.Enabled = false;
            RemoveFromListBtn.Enabled = false;
            Start.Text = "Stop";
        }


        private void UnlockUI()
        {
            outputPath.Enabled = true;
            addFileBtn.Enabled = true;
            chooseOutputBtn.Enabled = true;
            RemoveFromListBtn.Enabled = true;
            Start.Text = "Start";
        }
    }
}
