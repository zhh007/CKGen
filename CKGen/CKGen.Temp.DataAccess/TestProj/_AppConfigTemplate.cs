// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DataAccess.TestProj
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class AppConfigTemplate
    {
        public string ConnectionName;
        public string ConnectionString;

        public AppConfigTemplate(string connName, string connString)
        {
            if (string.IsNullOrEmpty(connName))
                throw new Exception("参数connName，命名空间不能为空。");

            this.ConnectionName = connName;

            if (string.IsNullOrEmpty(connString))
                throw new Exception("参数connString，命名空间不能为空。");

            this.ConnectionString = connString;
        }
    }
}
