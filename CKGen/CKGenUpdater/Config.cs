using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CKGenUpdater
{
    public class Config
    {
        public static string MainExe = ConfigurationManager.AppSettings["MainExe"];
        public static string GitHubUrl = ConfigurationManager.AppSettings["GitHubUrl"];
    }
}
