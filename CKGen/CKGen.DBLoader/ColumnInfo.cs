// -----------------------------------------------------------------------
// <copyright file="ColumnInfo.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.DBLoader
{
    using CKGen;
    using CKGen.DBSchema;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class ColumnInfo : IColumnInfo
    {
        private string _rawName;
        private string _name;
        private string _camelName;
        private string _pascalName;
        private string _lowerName;
        private string _dbType;
        private string _dbTargetType;
        private string _languagetype;
        private string _desc;
        private bool _nullable;
        private bool _isPrimaryKey;
        private int? _maxLength;
        private int? _precision;
        private int? _scale;
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
        /// 首字母小写格式，如columnName
        /// </summary>
        public string CamelName
        {
            get { return _camelName; }
        }

        /// <summary>
        /// 首字母大写格式，如ColumnName
        /// </summary>
        public string PascalName
        {
            get { return _pascalName; }
        }

        /// <summary>
        /// 全小写格式，如columnname
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
        /// sql类型，如datetime,varchar等
        /// </summary>
        public string DBType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// System.Data.SqlDbType类型
        /// </summary>
        public string DbTargetType
        {
            get { return _dbTargetType; }
            set { _dbTargetType = value; }
        }

        /// <summary>
        /// C#类型，如DateTime,string等
        /// </summary>
        public string LanguageType
        {
            get { return _languagetype; }
            set { _languagetype = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _desc; }
            set { _desc = value; }
        }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool Nullable
        {
            get { return _nullable; }
            set { _nullable = value; }
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
            set { _isPrimaryKey = value; }
        }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int? MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        /// <summary>
        /// 精度
        /// </summary>
        public int? Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        /// <summary>
        /// 小数位数
        /// </summary>
        public int? Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public bool Identity
        {
            get;
            set;
        }

        public bool Computed
        {
            get;
            set;
        }

        public bool HasStringLength
        {
            get
            {
                if (!this.Nullable && this.LanguageType == "string")
                {
                    if (this.MaxLength.HasValue)
                    {
                        if (this.MaxLength != 1073741823)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public Dictionary<string, string> Attributes
        {
            get { return _attr; }
            set { _attr = value; }
        }
    }
}
