using CKGen.Base;
using CKGen.DBSchema;
using CKGen.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CKGen
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DbTargetConvert.GetSqlDbType("int");
            LanguageConvert.GetCSharpTypeFromMSSQL("datetime");

            ServiceLocator.Instance.AddService<ICodeGenService>(new CodeGenService());
            
            Application.Run(new FrmMain());
        }
    }
}
