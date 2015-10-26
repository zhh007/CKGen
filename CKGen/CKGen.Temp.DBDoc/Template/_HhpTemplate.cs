// -----------------------------------------------------------------------
// <copyright file="_HhpTemplate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DBDoc.Template
{
    using CKGen.DBSchema;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class HhpTemplate
    {
        public List<ITableInfo> Tables;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="t"></param>
        public HhpTemplate(List<ITableInfo> t)
        {
            if (t == null)
                throw new Exception("参数t，不能为null。");

            this.Tables = t;


            //this.DBTable.Refresh();
        }
    }
}
