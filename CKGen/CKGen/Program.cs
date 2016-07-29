using CKGen.Base;
using CKGen.Base.Events;
using CKGen.Base.Log;
using CKGen.DBSchema;
using CKGen.Services;
using System;
using System.Collections.Generic;
using System.Threading;
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
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
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

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandlerException(e.Exception);
        }

        private static void HandlerException(Exception e)
        {
            if(e == null)
            {
                return;
            }
            try
            {
                LogHelper.Error(e, "未处理异常");
                DialogResult result = ShowExceptionDialog(e);

                if (result == DialogResult.Abort)
                    Application.Exit();
            }
            catch (Exception ex)
            {
                try
                {
                    LogHelper.Error(ex, "未处理异常");
                    MessageBox.Show("致命错误", "致命错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null && e.ExceptionObject is Exception)
            {
                HandlerException(e.ExceptionObject as Exception);
            }
        }

        private static DialogResult ShowExceptionDialog(Exception ex)
        {
            string errorMessage =
                    "Unhandled Exception:\n\n" +
                    ex.Message + "\n\n" +
                    ex.GetType() +
                    "\n\nStack Trace:\n" +
                    ex.StackTrace;

            return MessageBox.Show(errorMessage, "程序错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }
    }
}
