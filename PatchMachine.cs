using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64noAAPatcher
{
    enum CmdAppType
    {
        Patcher, CRC
    }

    class PatchMachine
    {
        static System.Diagnostics.Process cmdProcess;
        SafeProcessHandle cmdHandle;
        public bool isRunning;

        public Action OnFilesListChanged;
        //private Queue<string> filesToPatch;
        //for removing any items
        private List<string> filesToPatch;
        public PatchMachine() {
            filesToPatch = new List<string>();
        }

        public void AddFileToPatch(string path)
        {
            //if file exists
            filesToPatch.Add(path);
            OnFilesListChanged?.Invoke();
        }

        public void RemoveFileFromPatch(string path)
        {
            //if file exists
            filesToPatch.Remove(path);
            OnFilesListChanged?.Invoke();
        }

        public void RemoveFileFromPatch(int index)
        {
            //if file exists
            filesToPatch.RemoveAt(index);
            OnFilesListChanged?.Invoke();
        }

        public string GetNext()
        {
            var value = filesToPatch.First();
            filesToPatch.RemoveAt(0);
            return value;
        }

        public List<string> GetFilesToPatch()
        {
            return this.filesToPatch;
        }

        public void StartCmdProcess(CmdAppType cmdType, string strCmdText)
        {
            switch (cmdType)
            {
                case CmdAppType.Patcher:
                    StartConvertCmd(strCmdText);
                    break;
                case CmdAppType.CRC:
                    StartCrcCmd(strCmdText);
                    break;
            }
        }

        private void StartCmdProcess(string arguments)
        {
            try
            {
                if (cmdProcess != null) StopCmdProcess();
                cmdProcess = new System.Diagnostics.Process();
                cmdProcess.StartInfo.FileName = "CMD.exe";
                cmdProcess.StartInfo.Arguments = arguments;
                cmdProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                if (cmdProcess.Start())
                {
                    cmdHandle = cmdProcess.SafeHandle;
                    isRunning = true;
                }
                cmdProcess.WaitForExit(4000);
                StopCmdProcess();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void StartCrcCmd(string strCmdText)
        {
            StartCmdProcess($"/k cd {Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\\additionals && rn64crc.exe {strCmdText} -u");
        }

        private void StartConvertCmd(string strCmdText)
        {
            StartCmdProcess($"/k cd {Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\\additionals && u64aap.exe --df-off {strCmdText}");
            return;
            try
            {
                if (cmdProcess != null) StopCmdProcess();
                cmdProcess = new System.Diagnostics.Process();
                cmdProcess.StartInfo.FileName = "CMD.exe";
                cmdProcess.StartInfo.Arguments = $"/k cd {Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\\additionals && u64aap.exe {strCmdText}";
                cmdProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                if (cmdProcess.Start())
                {
                    cmdHandle = cmdProcess.SafeHandle;
                    isRunning = true;
                }
                cmdProcess.WaitForExit(6000);
                StopCmdProcess();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StopCmdProcess()
        {
            if (cmdProcess != null)
            {
                try
                {
                    cmdProcess?.CloseMainWindow();
                    cmdProcess?.Close();
                    cmdProcess?.Dispose();
                    cmdProcess = null;
                    isRunning = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unable to stop CMD process: {e.Message}");
                    cmdProcess = null;
                    isRunning = false;
                }

                try
                {
                    if (cmdHandle != null && !cmdHandle.IsClosed)
                    {
                        cmdHandle.Close();
                        cmdHandle.Dispose();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unable to dispose CMD descriptor: {e.Message}");
                    cmdHandle = null;
                }
            }
        }
    }
}
