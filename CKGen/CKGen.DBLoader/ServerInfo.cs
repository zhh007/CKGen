// -----------------------------------------------------------------------
// <copyright file="ServerInfo.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.DBLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Collections.ObjectModel;
    using CKGen.DBSchema;

    /// <summary>
    /// 服务器
    /// </summary>
    public class ServerInfo : IServerInfo
    {
        private string _name;
        private string _connection;
        private List<IDatabaseInfo> _databases = null;
        private SchemaLoader loader = null;
        private bool _databasesLoaded = false;
        private bool _hasConnected = false;

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
        public List<IDatabaseInfo> Databases
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
            this._databases = new List<IDatabaseInfo>();

            loader = new SchemaLoader(MyMeta.dbDriver.SQL, "SqlClient", "C#", this._connection);
        }

        public bool Connect()
        {
            _hasConnected = loader.Connect();
            return _hasConnected;
        }

        public void LoadDatabases()
        {
            if (_databasesLoaded)
                return;

            if (!_hasConnected)
            {
                Connect();
            }
            //loader.Connect();

            if (_hasConnected)
            {
                this._databases = new List<IDatabaseInfo>();
                foreach (MyMeta.Database db in loader.Root.Databases)
                {
                    string dbname = db.Name;
                    if (Array.IndexOf(loader.ignoreDatabaseName, dbname) != -1)
                        continue;
                    IDatabaseInfo item = new DatabaseInfo(this);
                    item.Name = dbname;
                    this._databases.Add(item);
                }

                _databasesLoaded = true;
            }
        }

        public IDatabaseInfo GetDatabase(string dbname)
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
