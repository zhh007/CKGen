using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBSchema
{
    public interface ITableInfo
    {
        /// <summary>
        /// 原始名称
        /// </summary>
        string RawName { get; set; }

        /// <summary>
        /// 首字母小写格式，如tableName
        /// </summary>
        string CamelName { get; }

        /// <summary>
        /// 首字母大写格式，如TableName
        /// </summary>
        string PascalName { get; }

        /// <summary>
        /// 全小写格式，如tablename
        /// </summary>
        string LowerName { get; }

        /// <summary>
        /// 有效的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 架构名称
        /// </summary>
        string Schema { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        /// <returns></returns>
        string Description { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        List<IColumnInfo> Columns { get; set; }

        List<IColumnInfo> Keys { get; }

        IDatabaseInfo Database { get; }

    }
}
