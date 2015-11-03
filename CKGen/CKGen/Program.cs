using CKGen.DBSchema;
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

            Application.Run(new FrmMain());
            //Application.Run(new Form2());

            
        }

        
    }
}
