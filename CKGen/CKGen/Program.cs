using CKGen.Base;
using CKGen.Base.Events;
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
            Bootstrap.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DbTargetConvert.GetSqlDbType("int");
            LanguageConvert.GetCSharpTypeFromMSSQL("datetime");

            ServiceLocator.Instance.AddService<ICodeGenService>(new CodeGenService());
            ServiceLocator.Instance.AddService<IResService>(new ResService());

            AppEvent.Subscribe<GetDbInstanceRequestEvent>(p =>
            {
                var evt = new GetDbInstanceResponseEvent()
                {
                    Token = p.Token,
                    Database = App.Instance.Database
                };
                AppEvent.Publish(evt);
            });

            Application.Run(new FrmMain());
        }
    }
}
