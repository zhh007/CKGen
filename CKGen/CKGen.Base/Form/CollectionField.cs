using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Form
{
    public class CollectionField : ModuleField
    {
        /// <summary>
        /// 关系实体
        /// </summary>
        public Module RelationshipEntity { get; private set; }
        /// <summary>
        /// 关系
        /// </summary>
        public ModuleRelationship Relationship { get; private set; }

        public CollectionField(Module parent, string title, Module entity, ModuleRelationship relationship)
            : base(parent, title, "", "")
        {
            this.RelationshipEntity = entity;
            this.Relationship = relationship;
        }
    }
}
