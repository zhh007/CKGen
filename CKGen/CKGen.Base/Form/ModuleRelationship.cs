using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Form
{
    /// <summary>
    /// 实体关系
    /// </summary>
    public class ModuleRelationship
    {
        /// <summary>
        /// 主键表
        /// </summary>
        public string PrimaryEntityName { get; set; }

        /// <summary>
        /// 主键表字段
        /// </summary>
        public string PrimaryEntityFieldName { get; set; }

        /// <summary>
        /// 主键表属性
        /// </summary>
        public string PrimaryEntityPropertyName { get; set; }

        /// <summary>
        /// 外键表
        /// </summary>
        public string ForeignEntityName { get; set; }

        /// <summary>
        /// 外键表字段
        /// </summary>
        public string ForeignEntityFieldName { get; set; }

        /// <summary>
        /// 外键表属性
        /// </summary>
        public string ForeignEntityPropertyName { get; set; }
    }
}
