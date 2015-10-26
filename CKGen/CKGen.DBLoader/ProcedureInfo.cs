// -----------------------------------------------------------------------
// <copyright file="ProcedureInfo.cs" company="Microsoft">
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
    /// TODO: Update summary.
    /// </summary>
    public class ProcedureInfo : IProcedureInfo
    {
        private string _name;
        private string _schema;
        private IDatabaseInfo _database;

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

        public IDatabaseInfo Database
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
