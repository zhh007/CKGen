using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Log
{
    public class LogHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("LogToFile");

        #region Debug
        public static void Debug(string message)
        {
            log.Debug(message);
        }

        public static void Debug(Exception ex, string message)
        {
            log.Debug(message + Environment.NewLine, ex);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            log.Debug(string.Format(format, args));
        }

        public static void DebugFormat(Exception ex, string format, params object[] args)
        {
            log.Debug(string.Format(format, args) + Environment.NewLine, ex);
        }
        #endregion

        #region Info
        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Info(Exception ex, string message)
        {
            log.Info(message + Environment.NewLine, ex);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            log.Info(string.Format(format, args));
        }

        public static void InfoFormat(Exception ex, string format, params object[] args)
        {
            log.Info(string.Format(format, args) + Environment.NewLine, ex);
        }
        #endregion

        #region Warn
        public static void Warn(string message)
        {
            log.Warn(message);
        }

        public static void Warn(Exception ex, string message)
        {
            log.Warn(message + Environment.NewLine, ex);
        }

        public static void WarnFormat(string format, params object[] args)
        {
            log.Warn(string.Format(format, args));
        }

        public static void WarnFormat(Exception ex, string format, params object[] args)
        {
            log.Warn(string.Format(format, args) + Environment.NewLine, ex);
        }
        #endregion

        #region Error
        public static void Error(Exception ex)
        {
            log.Error("", ex);
        }

        public static void Error(string message)
        {
            log.Error(message);
        }

        public static void Error(Exception ex, string message)
        {
            log.Error(message + Environment.NewLine, ex);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            log.Error(string.Format(format, args));
        }

        public static void ErrorFormat(Exception ex, string format, params object[] args)
        {
            log.Error(string.Format(format, args) + Environment.NewLine, ex);
        }
        #endregion

        #region Fatal
        public static void Fatal(string message)
        {
            log.Fatal(message);
        }

        public static void Fatal(Exception ex, string message)
        {
            log.Fatal(message + Environment.NewLine, ex);
        }

        public static void FatalFormat(string format, params object[] args)
        {
            log.Fatal(string.Format(format, args));
        }

        public static void FatalFormat(Exception ex, string format, params object[] args)
        {
            log.Fatal(string.Format(format, args) + Environment.NewLine, ex);
        }
        #endregion
    }
}
