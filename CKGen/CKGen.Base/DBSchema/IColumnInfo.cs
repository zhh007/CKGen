﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBSchema
{
    public interface IColumnInfo
    {
        /// <summary>
        /// 原始名称
        /// </summary>
        string RawName { get; set; }

        /// <summary>
        /// 首字母小写格式，如columnName
        /// </summary>
        string CamelName { get; }

        /// <summary>
        /// 首字母大写格式，如ColumnName
        /// </summary>
        string PascalName { get; }

        /// <summary>
        /// 全小写格式，如columnname
        /// </summary>
        string LowerName { get; }

        /// <summary>
        /// 有效的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// sql类型，如datetime,varchar等
        /// </summary>
        string DBType { get; set; }

        /// <summary>
        /// 完整sql类型，如nvarchar(50)等
        /// </summary>
        string DBFullType { get; set; }

        /// <summary>
        /// System.Data.SqlDbType类型
        /// </summary>
        string DbTargetType { get; set; }

        /// <summary>
        /// C#类型，如DateTime,string等
        /// </summary>
        string LanguageType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        bool Nullable { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        int? MaxLength { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        int? Precision { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        int? Scale { get; set; }

        bool Identity { get; set; }

        bool Computed { get; set; }

        bool HasStringLength { get; }

        string this[string key] { get; set; }
    }
}
