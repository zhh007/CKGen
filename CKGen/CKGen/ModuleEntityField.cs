using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen
{
    /// <summary>
    /// 1->1
    /// </summary>
    public class ModuleEntityField : ModuleFieldBase
    {
        public string FieldName { get; set; }
        public string CodeName { get; set; }


        public ModuleEntityField(ModuleEntity parent, string title)
            : base(parent, title)
        {

        }
    }

    public class ModuleCollectionEntityField : ModuleFieldBase
    {
        public string CodeName { get; set; }

        /// <summary>
        /// 关系实体
        /// </summary>
        public ModuleEntity RelationshipEntity { get; private set; }

        public ModuleCollectionEntityField(ModuleEntity parent, string title, ModuleEntity entity)
            : base(parent, title)
        {
            this.RelationshipEntity = entity;
        }
    }

    public class EntityRelationship
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
