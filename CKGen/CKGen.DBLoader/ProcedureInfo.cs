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
        private List<ISqlParameter> _parameters = new List<ISqlParameter>();
        private IDatabaseInfo _database;
        private SchemaLoader loader = null;
        private bool _paraLoaded = false;

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

        public List<ISqlParameter> Parameters
        {
            get
            {
                LoadParameters();
                return _parameters;
            }
            set { _parameters = value; }
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
            this.loader = database.Loader;
        }

        public void LoadParameters()
        {
            if (_paraLoaded)
                return;

            loader.Connect();

            this._parameters = new List<ISqlParameter>();
            MyMeta.IProcedure proc = loader.Root.Databases[this.Database.Name].Procedures[this.Name];
            foreach (MyMeta.IParameter para in proc.Parameters)
            {
                ISqlParameter item = new SqlParameter();
                item.Name = para.Name;
                item.DBType = para.TypeName;
                item.DBTypeFull = para.DataTypeNameComplete;
                item.LanguageType = para.LanguageType;
                item.DbTargetType = para.DbTargetType;
                item.MaxLength = para.CharacterMaxLength;
                item.Precision = para.NumericPrecision;
                item.Scale = para.NumericScale;
                item.Nullable = para.IsNullable;
                item.Description = para.Description;
                _parameters.Add(item);
            }

            _paraLoaded = true;
        }
    }
}
