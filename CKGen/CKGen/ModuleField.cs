using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen
{
    public class ModuleField : ModuleFieldBase
    {
        public string FieldName { get; set; }
        public string CodeName { get; set; }
        public bool IsKey{get;set;}
        public bool IsRequired { get; set; }
        public int? StringLength { get; set; }

        public ModuleField(ModuleEntity parent, string label, string name)
            : base(parent, label)
        {
            this.FieldName = name;
        }
    }
}
