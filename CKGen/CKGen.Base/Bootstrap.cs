using CKGen.Base.Log;
using CKGen.DBSchema;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base
{
    public class Bootstrap
    {
        private static bool _startWasCalled;

        public static void Start()
        {
            if (!_startWasCalled)
            {
                _startWasCalled = true;

                InitLog4net();
                //log4net.Config.XmlConfigurator.Configure();

                DbTargetConvert.GetSqlDbType("int");
                LanguageConvert.GetCSharpTypeFromMSSQL("datetime");
            }
        }

        private static void InitLog4net()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "[%date] %-5level [%thread] %property{user} - %message%exception%n";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = @"logs\";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 100;
            roller.MaximumFileSize = "10MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Date;
            roller.DatePattern = "yyyyMMdd'.log'";
            roller.StaticLogFileName = false;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

            log4net.GlobalContext.Properties["user"] = new Log4NetUserNameProvider();
        }
    }
}
