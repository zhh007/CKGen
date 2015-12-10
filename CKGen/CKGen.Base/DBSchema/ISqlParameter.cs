using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBSchema
{
    public interface ISqlParameter
    {
        string Name { get; set; }
        /// <summary>
        /// nvarchar
        /// </summary>
        string DBType { get; set; }
        /// <summary>
        /// nvarchar(255)
        /// </summary>
        string DBTypeFull { get; set; }
        /// <summary>
        /// string
        /// </summary>
        string LanguageType { get; set; }
        /// <summary>
        /// SqlDbType.NVarchar
        /// </summary>
        string DbTargetType { get; set; }
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
        /// <summary>
        /// 是否可空
        /// </summary>
        bool Nullable { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Description { get; set; }
    }
}
