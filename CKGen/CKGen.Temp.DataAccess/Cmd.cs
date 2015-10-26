// -----------------------------------------------------------------------
// <copyright file="Cmd.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;
    using System.Threading;

    public class ProcessEndEventArgs : EventArgs
    {
        public string key;
    }

    public delegate string ProcessEndEventHandler(object sender, ProcessEndEventArgs e);

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Cmd
    {
        private bool _start = false;
        private bool isRunEnd = false;
        private bool hasError = false;
        Process myProcess = null;

        private List<string> Output = new List<string>();
        private List<string> RawOutput = new List<string>();
        private List<string> ErrorOutput = new List<string>();

        public bool Start
        {
            get { return _start; }
        }

        public bool IsRunEnd
        {
            get { return isRunEnd; }
        }

        public bool HasError
        {
            get { return hasError; }
        }

        public Process MyProcess
        {
            get { return myProcess; }
        }

        public ProcessEndEventHandler ProcessEnd;

        protected virtual void OnProcessEnd()
        {
            Console.WriteLine("OnProcessEnd...");
            if (ProcessEnd != null)
            {
                ProcessEndEventArgs args = new ProcessEndEventArgs();

                ProcessEnd(this, args);
            }
        }

        public void Execute(string exePath, string args, int? milliseconds, string workPath)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = exePath;
                startInfo.Arguments = args;

                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.ErrorDialog = false;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                if (!string.IsNullOrEmpty(workPath))
                {
                    startInfo.WorkingDirectory = workPath;
                }

                myProcess = new Process();
                myProcess.StartInfo = startInfo;
                myProcess.EnableRaisingEvents = true;

                myProcess.Exited += new EventHandler(myProcess_Exited);
                myProcess.OutputDataReceived += new DataReceivedEventHandler(myProcess_OutputDataReceived);
                myProcess.ErrorDataReceived += new DataReceivedEventHandler(myProcess_ErrorDataReceived);

                myProcess.Start();
                myProcess.BeginErrorReadLine();
                myProcess.BeginOutputReadLine();

                _start = true;
                Debug.WriteLine("线程开始");

                //try
                //{
                //    myProcess.WaitForInputIdle();
                //}
                //catch (Exception)
                //{
                //}

                //if (this._asynCall)//异步调用
                //    myProcess.WaitForExit(5000);
                //else
                //myProcess.WaitForExit();

                if (milliseconds.HasValue)
                    myProcess.WaitForExit(milliseconds.Value);
                else
                    myProcess.WaitForExit();

                Debug.WriteLine("线程退出。");
            }
            catch (Exception ex)
            {

            }
        }

        public void Execute(string exePath, string args, int? millisecond)
        {
            Execute(exePath, args, millisecond, null);
        }

        public void Execute(string exePath, string args)
        {
            Execute(exePath, args, null, null);
        }

        public void Execute(string command, int? millisecond)
        {
            Execute("cmd", "/c " + command, millisecond);
        }

        public void Execute(string command)
        {
            Execute("cmd", "/c " + command);
        }

        void myProcess_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("Exited...");
            if (this.isRunEnd)
            {
                this.Close();
                return;
            }
            this.isRunEnd = true;
            this.Close();

            OnProcessEnd();
        }

        void myProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("ErrorDataReceived...");
            if (null == e) return;
            if (null == e.Data) return;

            this.ErrorOutput.Add(e.Data);

            Console.WriteLine(e.Data);
        }

        void myProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("OutputDataReceived...");
            if (null == e) return;
            if (null == e.Data) return;

            this.RawOutput.Add(e.Data);

            Console.WriteLine(e.Data);
        }

        public void Close()
        {
            try
            {
                //log.Write("close begin!");
                if (myProcess != null)
                {
                    //if (!myProcess.HasExited && myProcess.Id > 0)
                    //{
                    myProcess.Close();
                    //}
                }

                isRunEnd = true;

                //log.Write("close end!");
            }
            catch (Exception ex)
            {

                //myProcess.Kill();
            }
        }

        ///// <summary>
        ///// Executes a shell command synchronously.
        ///// </summary>
        ///// <param name="command">string command</param>
        ///// <returns>string, as output of the command.</returns>
        //public void ExecuteSync(object command)
        //{
        //    try
        //    {
        //        // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
        //        // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
        //        System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
        //        // The following commands are needed to redirect the standard output. 
        //        //This means that it will be redirected to the Process.StandardOutput StreamReader.
        //        procStartInfo.RedirectStandardOutput = true;
        //        procStartInfo.UseShellExecute = false;
        //        // Do not create the black window.
        //        procStartInfo.CreateNoWindow = true;
        //        // Now we create a process, assign its ProcessStartInfo and start it
        //        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        //        proc.StartInfo = procStartInfo;
        //        proc.Start();

        //        // Get the output into a string
        //        string result = proc.StandardOutput.ReadToEnd();

        //        // Display the command output.
        //        Console.WriteLine(result);
        //    }
        //    catch (Exception objException)
        //    {
        //        // Log the exception
        //    }
        //}

        /// <summary>
        /// Execute the command Asynchronously.
        /// </summary>
        /// <param name="command">string command.</param>
        public void ExecuteAsync(string command)
        {
            try
            {
                ParameterizedThreadStart param = new ParameterizedThreadStart(
                    delegate(object d)
                    {
                        Execute(command);
                    }
                );
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(param);
                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;
                //Start the thread.
                objThread.Start(command);
            }
            catch (ThreadStartException objException)
            {
                // Log the exception
            }
            catch (ThreadAbortException objException)
            {
                // Log the exception
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }
    }
}
