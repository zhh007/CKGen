using CKGen.Base;
using CKGen.Base.Events;
using CKGen.Base.Log;
using CKGen.DBSchema;
using CKGen.Services;
using System;
using System.Collections.Generic;
using System.Text;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /**
             * 当前用户是管理员的时候，直接启动应用程序
             * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
             */
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //如果是管理员，则直接运行
                //Application.Run(new Form1());
                LoadMain();
            }
            else
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    return;
                }
                //退出
                Application.Exit();
            }
        }

        private static void LoadMain()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Bootstrap.Start();

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
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("未处理异常");
                if(e is System.Reflection.ReflectionTypeLoadException)
                {
                    var ex = e as System.Reflection.ReflectionTypeLoadException;
                    if(ex != null)
                    {
                        foreach (var item in ex.LoaderExceptions)
                        {
                            sb.AppendLine(item.Message);
                        }
                    }
                }
                LogHelper.Error(e, sb.ToString());
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
