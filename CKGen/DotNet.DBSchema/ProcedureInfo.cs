// -----------------------------------------------------------------------
// <copyright file="ProcedureInfo.cs" company="Microsoft">
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
    /// TODO: Update summary.
    /// </summary>
    public class ProcedureInfo
    {
        private string _name;
        private string _schema;
        private DatabaseInfo _database;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        public DatabaseInfo Database
        {
            get { return _database; }
        }

        internal ProcedureInfo()
        {

        }

        internal ProcedureInfo(DatabaseInfo database)
        {
            this._database = database;
        }
    }
}
