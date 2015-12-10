using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBLoader
{
    public class SqlParameter : ISqlParameter
    {
        public string Name { get; set; }
        /// <summary>
        /// nvarchar
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// nvarchar(255)
        /// </summary>
        public string FullDataType { get; set; }
        /// <summary>
        /// string
        /// </summary>
        public string LanguageType { get; set; }
        /// <summary>
        /// SqlDbType.NVarchar
        /// </summary>
        public string DbTargetType { get; set; }
        /// <summary>
        /// 最大长度
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public int? Precision { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public int? Scale { get; set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool Nullable { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
    }
}
