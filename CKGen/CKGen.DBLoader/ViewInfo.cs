// -----------------------------------------------------------------------
// <copyright file="ViewInfo.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.DBLoader
{
    using CKGen.DBSchema;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 视图
    /// </summary>
    public class ViewInfo : IViewInfo
    {
        private string name;
        private string schema;
        private List<IColumnInfo> columns = new List<IColumnInfo>();
        private IDatabaseInfo _database;

        public string Schema
        {
            get { return schema; }
            set { schema = value; }
        }        

        public string Name
        {
            get { return name; }
            set { name = value; }
        }        

        public List<IColumnInfo> Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public IDatabaseInfo Database
        {
            get { return _database; }
        }

        internal ViewInfo()
        {

        }

        internal ViewInfo(DatabaseInfo database)
        {
            this._database = database;
        }
    }
}
