// -----------------------------------------------------------------------
// <copyright file="SchemaLoader.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.DBLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Reflection;
    using DBSchema;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SchemaLoader
    {
        internal string[] ignoreDatabaseName = new string[] { "master", "model", "msdb", "tempdb" };
        internal string[] ignoreProcName = new string[] { 
            "sp_alterdiagram", "sp_creatediagram", "sp_dropdiagram", 
            "sp_helpdiagramdefinition", "sp_helpdiagrams", "sp_renamediagram", "sp_upgraddiagrams",
            "fn_diagramobjects" };

        private string _DbTargetMappingFileName = Path.Combine(Environment.CurrentDirectory, @"Settings\DbTargets.xml");
        private string _LanguageMappingFileName = Path.Combine(Environment.CurrentDirectory, @"Settings\Languages.xml");
        private string _DbTarget = "";
        private string _Language = "";
        private string _ConnectionString = "";
        private MyMeta.dbDriver _dbDriver;
        private MyMeta.dbRoot _root = null;

        public MyMeta.dbRoot Root
        {
            get
            {
                return _root;
            }
        }

        public SchemaLoader(MyMeta.dbDriver dbDriver, string dbTarget, string language, string connectionString)
        {
            string settingFolder = Path.Combine(Environment.CurrentDirectory, @"Settings");

            if (!System.IO.Directory.Exists(settingFolder))
            {
                System.IO.Directory.CreateDirectory(settingFolder);
            }

            if (!System.IO.File.Exists(this._DbTargetMappingFileName))
            {
                using (Stream input = Assembly.GetAssembly(typeof(IDatabaseInfo)).GetManifestResourceStream("CKGen.Base.Res.DbTargets.xml"))
                using (Stream output = File.Create(this._DbTargetMappingFileName))
                {
                    byte[] buffer = new byte[8192];

                    int bytesRead;
                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, bytesRead);
                    }
                }
            }

            if (!System.IO.File.Exists(this._LanguageMappingFileName))
            {
                using (Stream input = Assembly.GetAssembly(typeof(IDatabaseInfo)).GetManifestResourceStream("CKGen.Base.Res.Languages.xml"))
                using (Stream output = File.Create(this._LanguageMappingFileName))
                {
                    byte[] buffer = new byte[8192];

                    int bytesRead;
                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, bytesRead);
                    }
                }
            }

            _DbTarget = dbTarget;
            _Language = language;
            _ConnectionString = connectionString;

            _root = new MyMeta.dbRoot();
            _root.DbTargetMappingFileName = this._DbTargetMappingFileName;
            _root.DbTarget = this._DbTarget;

            _root.LanguageMappingFileName = this._LanguageMappingFileName;
            _root.Language = this._Language;
            

            //_root.ShowSystemData = true;

            _dbDriver = dbDriver;

        }

        public bool Connect()
        {
            if (_connected)
                return true;
            _connected = _root.Connect(this._dbDriver, this._ConnectionString);

            //_connected = true;

            //_root.Driver = this._dbDriver;
            _root.DbTargetMappingFileName = this._DbTargetMappingFileName;
            _root.DbTarget = this._DbTarget;

            _root.LanguageMappingFileName = this._LanguageMappingFileName;
            _root.Language = this._Language;

            return _connected;
        }

        private bool _connected = false;
        
        public List<DatabaseInfo> LoadDatabases()
        {
            this.Connect();

            List<DatabaseInfo> dbs = new List<DatabaseInfo>();
            foreach (MyMeta.Database db in _root.Databases)
            {
                string dbname = db.Name;
                if (Array.IndexOf(this.ignoreDatabaseName, dbname) != -1)
                    continue;
                DatabaseInfo item = new DatabaseInfo();
                item.Name = dbname;
                dbs.Add(item);
            }

            return dbs;
        }

        public List<TableInfo> LoadTables(string databaseName)
        {
            this.Connect();

            List<TableInfo> tables = new List<TableInfo>();
            foreach (MyMeta.Table table in _root.Databases[databaseName].Tables)
            {
                string tablename = table.Name;

                //if (Array.IndexOf(this.ignoreDatabaseName, dbname) != -1)
                //    continue;
                TableInfo item = new TableInfo();
                item.RawName = tablename;
                //item.Loader = this;
                tables.Add(item);
            }

            return tables;
        }

        public List<ViewInfo> LoadViews(string databaseName)
        {
            this.Connect();

            List<ViewInfo> views = new List<ViewInfo>();
            foreach (MyMeta.View view in _root.Databases[databaseName].Views)
            {
                string tablename = view.Name;
                //if (Array.IndexOf(this.ignoreDatabaseName, dbname) != -1)
                //    continue;
                ViewInfo item = new ViewInfo();
                item.Name = tablename;
                //item.Loader = this;
                views.Add(item);
            }

            return views;
        }

        public List<ProcedureInfo> LoadProcedures(string databaseName)
        {
            this.Connect();

            List<ProcedureInfo> views = new List<ProcedureInfo>();
            foreach (MyMeta.Procedure proc in _root.Databases[databaseName].Procedures)
            {
                if (proc.Schema == "sys")
                    continue;

                string pname = proc.Name;
                if (Array.IndexOf(this.ignoreProcName, pname) != -1)
                    continue;
                ProcedureInfo item = new ProcedureInfo();
                item.Name = pname;
                //item.Loader = this;
                views.Add(item);
            }

            return views;
        }


    }
}
