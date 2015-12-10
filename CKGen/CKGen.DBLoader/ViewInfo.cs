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
        private string _rawName;
        private string _name;
        private string _camelName;
        private string _pascalName;
        private string _lowerName;
        private string _schema;
        private string _desc;
        private List<IColumnInfo> _columns = new List<IColumnInfo>();
        private IDatabaseInfo _database;
        private SchemaLoader loader = null;
        private bool _columnsLoaded = false;
        private Dictionary<string, string> _attr = new Dictionary<string, string>();

        /// <summary>
        /// 原始名称
        /// </summary>
        public string RawName
        {
            get { return _rawName; }
            set
            {
                _rawName = value;
                _name = StringHelper.SetValidName(_rawName);
                _camelName = StringHelper.SetCamelCase(_rawName);
                _pascalName = StringHelper.SetPascalCase(_rawName);
                _lowerName = _camelName.ToLower();
            }
        }

        /// <summary>
        /// 首字母小写格式，如tableName
        /// </summary>
        public string CamelName
        {
            get { return _camelName; }
        }

        /// <summary>
        /// 首字母大写格式，如TableName
        /// </summary>
        public string PascalName
        {
            get { return _pascalName; }
        }

        /// <summary>
        /// 全小写格式，如tablename
        /// </summary>
        public string LowerName
        {
            get { return _lowerName; }
        }

        /// <summary>
        /// 有效的名称
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 架构名称
        /// </summary>
        public string Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            get { return _desc; }
            set { _desc = value; }
        }

        internal SchemaLoader Loader
        {
            get { return this.loader; }
        }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<IColumnInfo> Columns
        {
            get
            {
                LoadColumns();
                return _columns;
            }
            set { _columns = value; }
        }

        public Dictionary<string, string> Attributes
        {
            get { return _attr; }
            set { _attr = value; }
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
            this.loader = database.Loader;
        }

        public void LoadColumns()
        {
            if (_columnsLoaded)
                return;

            loader.Connect();

            this._columns = new List<IColumnInfo>();
            MyMeta.IView view = loader.Root.Databases[this.Database.Name].Views[this.RawName];
            foreach (MyMeta.Column item in view.Columns)
            {
                string columnName = item.Name;

                IColumnInfo cInfo = new ColumnInfo();
                cInfo.RawName = columnName;

                //string dbtype = "";
                //string sqlDbType = item.DataType.SqlDataType.ToString();
                //if (item.DataType.SqlDataType == SqlDataType.UserDefinedDataType)
                //{
                //    dbtype = Util.GetUserDefinedDataType(item.DataType.Name, "dbo", this._dbConnection);

                //    try
                //    {
                //        SqlDataType sdt = (SqlDataType)Enum.Parse(typeof(SqlDataType), dbtype, true);
                //        sqlDbType = sdt.ToString();
                //    }
                //    catch (Exception)
                //    {
                //        sqlDbType = dbtype;
                //    }
                //}
                //else
                //{
                //    dbtype = item.DataType.Name;
                //}

                cInfo.DBType = item.DataTypeName;//dbtype;
                cInfo.DbTargetType = item.DbTargetType;//sqlDbType;
                cInfo.CSharpType = LanguageConvert.GetCSharpTypeFromMSSQL(cInfo.DBType, item.IsNullable);
                cInfo.Nullable = item.IsNullable;
                cInfo.Description = item.Description;
                cInfo.IsPrimaryKey = item.IsInPrimaryKey;
                cInfo.MaxLength = item.CharacterMaxLength;
                cInfo.Precision = item.NumericPrecision;
                cInfo.Scale = item.NumericScale;
                cInfo.Identity = item.IsAutoKey;
                cInfo.Computed = item.IsComputed;

                this._columns.Add(cInfo);
            }

            _columnsLoaded = true;
        }
    }
}
