using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet.DbConsoleProject
{
    public class DbConsoleProjectModel
    {
        public string ProjectName { get; set; }
        /// <summary>
        /// 大写
        /// </summary>
        public string ProjectGuid { get; set; }
        /// <summary>
        /// 小写
        /// </summary>
        public string ProjectGuidLower { get; set; }
        public string ConnectionString { get; set; }
        public List<string> Files { get; set; }
        public DbConsoleProjectModel()
        {
            this.Files = new List<string>();
        }
    }
}
