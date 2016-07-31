// -----------------------------------------------------------------------
// <copyright file="DBDocBuilder.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DBDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.SqlClient;
    using System.IO;
    using System.Diagnostics;
    using DBSchema;
    using Template;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DBDocBuilder
    {
        private IDatabaseInfo _database;
        private string _targetFolder;
        public string TargetFolder
        {
            get { return _targetFolder; }
        }

        public DBDocBuilder(IDatabaseInfo database)
        {
            if (database == null)
                throw new Exception("数据库不能为空。");

            this._database = database;
            Guid folderID = Guid.NewGuid();
            this._targetFolder = Path.Combine(Path.GetTempPath(), folderID.ToString("P"));
            Directory.CreateDirectory(_targetFolder);

            try
            {
                if (!Directory.Exists(this._targetFolder))
                {
                    Directory.CreateDirectory(this._targetFolder);
                }
                else
                {
                    Directory.Delete(this._targetFolder, true);
                    Directory.CreateDirectory(this._targetFolder);
                }
            }
            catch (Exception)
            {
                throw new Exception("目标文件夹创建失败。");
            }
        }

        public void Build()
        {
            List<string> filePaths = new List<string>();
            foreach (ITableInfo table in _database.Tables)
            {
                TableDocTemplete autoCode = new TableDocTemplete(table);
                string autoCodeStr = autoCode.TransformText();
                string autoFolder = Path.Combine(this._targetFolder, "Tables");
                string autoFilePath = Path.Combine(autoFolder, table.RawName + ".html");
                if (!Directory.Exists(autoFolder))
                {
                    Directory.CreateDirectory(autoFolder);
                }

                using (StreamWriter sw = File.CreateText(autoFilePath))
                {
                    sw.Write(autoCodeStr);
                    sw.Close();
                }
                filePaths.Add(autoFilePath);
            }

            //hhp
            HhpTemplate hhp = new HhpTemplate(_database.Tables);
            string hhpText = hhp.TransformText();
            string hhpFolder = this._targetFolder;
            string hhpFilePath = Path.Combine(hhpFolder, "test.hhp");
            if (!Directory.Exists(hhpFolder))
            {
                Directory.CreateDirectory(hhpFolder);
            }

            FileStream fs = new FileStream(hhpFilePath, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB18030")))
            {
                sw.Write(hhpText);
                sw.Close();
            }

            //hhk
            HhkTemplate hhk = new HhkTemplate(_database.Tables);
            string hhkText = hhk.TransformText();
            string hhkFolder = this._targetFolder;
            string hhkFilePath = Path.Combine(hhkFolder, "test.hhk");
            if (!Directory.Exists(hhkFolder))
            {
                Directory.CreateDirectory(hhkFolder);
            }

            fs = new FileStream(hhkFilePath, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB18030")))
            {
                sw.Write(hhkText);
                sw.Close();
            }

            //hhc
            HhcTemplate hhc = new HhcTemplate(_database.Tables);
            string hhcText = hhc.TransformText();
            string hhcFolder = this._targetFolder;
            string hhcFilePath = Path.Combine(hhcFolder, "test.hhc");
            if (!Directory.Exists(hhcFolder))
            {
                Directory.CreateDirectory(hhcFolder);
            }

            fs = new FileStream(hhcFilePath, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB18030")))
            {
                sw.Write(hhcText);
                sw.Close();
            }

            //MainPage
            MainPageTemplate mpage = new MainPageTemplate();
            string mpText = mpage.TransformText();
            string mpFolder = this._targetFolder;
            string mpFilePath = Path.Combine(mpFolder, "index.html");
            if (!Directory.Exists(mpFolder))
            {
                Directory.CreateDirectory(mpFolder);
            }

            fs = new FileStream(mpFilePath, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("utf-8")))
            {
                sw.Write(mpText);
                sw.Close();
            }

            string _chmFile = Path.Combine(this._targetFolder, "test.chm");
            try
            {
                if (File.Exists(_chmFile))
                {
                    File.Delete(_chmFile);
                }
            }
            catch
            {
                throw new Exception("文件被打开！");
            }

            CHMBuilder chmBuilder = new CHMBuilder();
            chmBuilder.Compile(hhpFilePath);

            #region old chm builder
            //string hhcFile = @"C:\Program Files (x86)\HTML Help Workshop\hhc.exe"; //hhc.exe文件位置，windows自带的，一般是这个路径
            //Process helpCompileProcess = new Process();  //创建新的进程，用Process启动HHC.EXE来Compile一个CHM文件
            //ProcessStartInfo processStartInfo = new ProcessStartInfo();
            //processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //processStartInfo.FileName = hhcFile;  //调入HHC.EXE文件 
            //processStartInfo.Arguments = "\"" + hhpFilePath + "\"";//获取空的HHP文件
            //processStartInfo.UseShellExecute = false;
            //helpCompileProcess.StartInfo = processStartInfo;
            //helpCompileProcess.Start();
            //helpCompileProcess.WaitForExit(); //组件无限期地等待关联进程退出

            //if (helpCompileProcess.ExitCode == 0)
            //{
            //    MessageBox.Show(new Exception().Message);                    
            //}
            //helpCompileProcess.Close(); 
            #endregion

            //return filePaths;
        }

    }
}
