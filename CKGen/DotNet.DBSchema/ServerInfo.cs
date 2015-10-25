// -----------------------------------------------------------------------
// <copyright file="ServerInfo.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.DBSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Collections.ObjectModel;

    /// <summary>
    /// 服务器
    /// </summary>
    public class ServerInfo
    {
        private string _name;
        private string _connection;
        private List<DatabaseInfo> _databases = null;
        private SchemaLoader loader = null;
        private bool _databasesLoaded = false;

        /// <summary>
        /// 服务器名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }        

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }        

        /// <summary>
        /// 数据库
        /// </summary>
        public List<DatabaseInfo> Databases
        {
            get 
            {
                this.LoadDatabases();
                return _databases; 
            }
        }

        internal SchemaLoader Loader
        {
            get { return this.loader; }
        }

        public ServerInfo(string conn, string name)
        {
            this._connection = conn;
            this._name = name;
            this._databases = new List<DatabaseInfo>();

            loader = new SchemaLoader(MyMeta.dbDriver.SQL, "SqlClient", "C#", this._connection);
        }
        
        public void LoadDatabases()
        {
            if (_databasesLoaded)
                return;

            loader.Connect();

            this._databases = new List<DatabaseInfo>();
            foreach (MyMeta.Database db in loader.Root.Databases)
            {
                string dbname = db.Name;
                if (Array.IndexOf(loader.ignoreDatabaseName, dbname) != -1)
                    continue;
                DatabaseInfo item = new DatabaseInfo(this);
                item.Name = dbname;
                this._databases.Add(item);
            }

            _databasesLoaded = true;
        }

        public DatabaseInfo GetDatabase(string dbname)
        {
            foreach (var item in this.Databases)
            {
                if (item.Name == dbname)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
