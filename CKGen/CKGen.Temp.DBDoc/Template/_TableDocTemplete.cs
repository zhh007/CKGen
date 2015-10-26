// -----------------------------------------------------------------------
// <copyright file="_TableDocTemplete.cs" company="Microsoft">
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
    public partial class TableDocTemplete
    {
        public ITableInfo DBTable;

        public string TableName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="t"></param>
        public TableDocTemplete(ITableInfo t)
        {
            if (t == null)
                throw new Exception("参数t，不能为null。");

            this.DBTable = t;

            this.TableName = this.DBTable.RawName;



            //this.DBTable.Refresh();
        }
    }
}
