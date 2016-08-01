using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CKGenUpdater
{
    public partial class Form1 : Form
    {
        private string DownloadUrl { get; set; }
        private string FilePath { get; set; }
        private string NewVersion { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.btnCancel.Enabled = true;
            this.btnOk.Enabled = false;

            string assFile = Path.Combine(Environment.CurrentDirectory, Config.MainExe);
            var v = Assembly.LoadFile(assFile).GetName().Version;
            string assemblyVersion = string.Format("v{0}.{1}", v.Major, v.Minor);
            GitHubReleaseInfo info = new GitHubReleaseInfo(Config.GitHubUrl);

            this.NewVersion = info.Version;
            this.DownloadUrl = info.DownloadUrl;
            this.Text = "版本" + assemblyVersion + " ==> " + info.Version;
            this.lblFile.Text = "文件名：" + Path.GetFileName(info.DownloadUrl);
            var s = info.Body.Split(new string[] { "\\r", "\\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in s)
            {
                this.txtBody.AppendText(item + Environment.NewLine);
            }

            if(this.NewVersion != assemblyVersion)
            {
                this.btnOk.Enabled = true;
            }
            else
            {
                this.Text = "不需要更新。";
                this.txtBody.Clear();
                this.lblFile.Text = "";
                this.lblFile.Hide();
                this.progressBar1.Hide();
                this.label1.Hide();
                this.txtBody.Hide();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            Guid folderID = Guid.NewGuid();
            string dir = Path.Combine(Path.GetTempPath(), folderID.ToString("P"));

            //string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder., "Update");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            this.FilePath = Path.Combine(dir, Path.GetFileName(this.DownloadUrl));
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            client.DownloadFileAsync(new Uri(this.DownloadUrl), FilePath);
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string rootDir = Path.GetDirectoryName(this.FilePath);
            string unzipFolder = Path.Combine(rootDir, this.NewVersion);
            if (Directory.Exists(unzipFolder))
            {
                Directory.Delete(unzipFolder, true);
            }
            Directory.CreateDirectory(unzipFolder);
            ZipHelp.UnZip(this.FilePath, unzipFolder);

            string sourceFolder = "";
            var files = Directory.EnumerateFiles(unzipFolder, Config.MainExe, SearchOption.AllDirectories);
            if (files != null && files.Count() == 1)
            {
                var targetFileName = files.FirstOrDefault();
                sourceFolder = Path.GetDirectoryName(targetFileName);
            }

            string targetDir = Environment.CurrentDirectory;
            foreach (var process in Process.GetProcessesByName("CKGen"))
            {
                process.Kill();
            }

            RenameAllFile(targetDir);
            //copy 
            ProcessXcopy(sourceFolder, targetDir);

            Process.Start(Path.Combine(Environment.CurrentDirectory, Config.MainExe));

            this.Close();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method to Perform Xcopy to copy files/folders from Source machine to Target Machine
        /// </summary>
        /// <param name="SolutionDirectory"></param>
        /// <param name="TargetDirectory"></param>
        private static void ProcessXcopy(string SolutionDirectory, string TargetDirectory)
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = true;
            //Give the name as Xcopy
            startInfo.FileName = "xcopy";
            //make the window Hidden
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //Send the Source and destination as Arguments to the process
            startInfo.Arguments = "\"" + SolutionDirectory + "\"" + " " + "\"" + TargetDirectory + "\"" + @" /e /y /I";
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        private void RenameAllFile(string dir)
        {
            var files = Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly)
                                    .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));
            foreach (var file in files)
            {
                string newName = file + ".old";
                if (File.Exists(newName))
                {
                    File.Delete(newName);
                }
                File.Move(file, newName);
            }
        }
    }
}
