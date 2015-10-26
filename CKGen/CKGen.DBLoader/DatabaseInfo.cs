// -----------------------------------------------------------------------
// <copyright file="DatabaseInfo.cs" company="Microsoft">
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
    /// 数据库
    /// </summary>
    public class DatabaseInfo : IDatabaseInfo
    {
        private string name;
        private int dbid;
        private DateTime createtime;
        private SchemaLoader loader = null;
        private List<ITableInfo> tables = new List<ITableInfo>();
        private List<IViewInfo> views = new List<IViewInfo>();
        private List<IProcedureInfo> procs = new List<IProcedureInfo>();
        private bool _tablesLoaded = false;
        private bool _viewsLoaded = false;
        private bool _procLoaded = false;
        private IServerInfo _server = null;

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }        

        /// <summary>
        /// 数据库id
        /// </summary>
        public int Dbid
        {
            get { return dbid; }
            set { dbid = value; }
        }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime
        {
            get { return createtime; }
            set { createtime = value; }
        }

        /// <summary>
        /// 表
        /// </summary>
        public List<ITableInfo> Tables
        {
            get 
            {
                this.LoadTables();
                return tables;
            }
        }        

        /// <summary>
        /// 视图
        /// </summary>
        public List<IViewInfo> Views
        {
            get 
            {
                this.LoadViews();
                return views; 
            }
        }

        /// <summary>
        /// 存储过程
        /// </summary>
        public List<IProcedureInfo> Procedures
        {
            get
            {
                this.LoadProcedures();
                return procs;
            }
        }

        /// <summary>
        /// 服务器
        /// </summary>
        public IServerInfo Server
        {
            get { return _server; }
        }

        internal SchemaLoader Loader
        {
            get { return this.loader; }
        }

        internal DatabaseInfo()
        {
            
        }

        internal DatabaseInfo(ServerInfo server)
        {
            this._server = server;
            this.loader = server.Loader;
        }

        #region 私有方法
        private void LoadTables()
        {
            if (_tablesLoaded)
                return;

            loader.Connect();

            this.tables = new List<ITableInfo>();
            foreach (MyMeta.Table table in loader.Root.Databases[this.Name].Tables)
            {
                string tablename = table.Name;
                //if (Array.IndexOf(this.ignoreDatabaseName, dbname) != -1)
                //    continue;

                ITableInfo item = new TableInfo(this);
                item.RawName = tablename;
                item.Schema = table.Schema;
                item.Description = table.Description;

                this.tables.Add(item);
            }

            _tablesLoaded = true;
        }

        private void LoadViews()
        {
            if (_viewsLoaded)
                return;

            loader.Connect();

            this.views = new List<IViewInfo>();
            foreach (MyMeta.View view in loader.Root.Databases[this.Name].Views)
            {
                string tablename = view.Name;
                //if (Array.IndexOf(this.ignoreDatabaseName, dbname) != -1)
                //    continue;

                IViewInfo item = new ViewInfo(this);
                item.Name = tablename;
                item.Schema = view.Schema;
                this.views.Add(item);
            }

            _viewsLoaded = true;
        }

        private void LoadProcedures()
        {
            if (_procLoaded)
                return;

            loader.Connect();

            this.procs = new List<IProcedureInfo>();
            foreach (MyMeta.Procedure proc in loader.Root.Databases[this.Name].Procedures)
            {
                if (proc.Schema == "sys")
                    continue;

                string pname = proc.Name;
                if (Array.IndexOf(loader.ignoreProcName, pname) != -1)
                    continue;

                IProcedureInfo item = new ProcedureInfo(this);
                item.Name = pname;
                item.Schema = proc.Schema;
                procs.Add(item);
            }
            //this.procs = loader.LoadProcedures(this.name);
            _procLoaded = true;
        } 
        #endregion
    }
}
