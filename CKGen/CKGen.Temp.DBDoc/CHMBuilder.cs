// -----------------------------------------------------------------------
// <copyright file="CHMBuilder.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DBDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CHMBuilder
    {
        string log1;
        string log2;

        delegate bool GetInfo(string log);

        //编译信息
        public bool GetInfo1(string log)
        {
            log1 = log;
            return true;
        }

        //进度信息
        public bool GetInfo2(string log)
        {
            log2 = log;
            return true;
        }

        [DllImport("hha.dll")]
        private extern static void HHA_CompileHHP(string hhpFile, GetInfo g1, GetInfo g2, int stack);

        public void Compile(string hhpFilePath)
        {
            HHA_CompileHHP(hhpFilePath, GetInfo1, GetInfo2, 0);
            //using (OpenFileDialog ofd = new OpenFileDialog())
            //{
            //    ofd.Filter = "CHM项目文件|*.hhp";
            //    ofd.ShowDialog();
            //    if (ofd.FileName != "")
            //    {
            //        HHA_CompileHHP(ofd.FileName, GetInfo1, GetInfo2, 0);
            //        MessageBox.Show("编译成功");
            //    }
            //}
        }
    }
}
