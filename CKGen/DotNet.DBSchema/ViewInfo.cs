// -----------------------------------------------------------------------
// <copyright file="ViewInfo.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.DBSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 视图
    /// </summary>
    public class ViewInfo
    {
        private string name;
        private string schema;
        private List<ColumnInfo> columns = new List<ColumnInfo>();
        private DatabaseInfo _database;

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

        public List<ColumnInfo> Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public DatabaseInfo Database
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
