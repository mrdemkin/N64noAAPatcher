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
    class PatchMachine
    {
        static System.Diagnostics.Process cmdProcess;
        SafeProcessHandle cmdHandle;
        public bool isRunning;

        public Action OnFilesListChanged;
        private Queue<string> filesToPatch;
        public PatchMachine() {
            filesToPatch = new Queue<string>();
        }

        public void AddFileToPatch(string path)
        {
            //if file exists
            filesToPatch.Enqueue(path);
            OnFilesListChanged?.Invoke();
        }

        public string GetNext()
        {
            return filesToPatch.Dequeue();
        }

        public Queue<string> GetFilesToPatch()
        {
            return this.filesToPatch;
        }

        public void StartCmdProcess(string strCmdText)
        {
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
                cmdProcess.WaitForExit(11000);
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
