using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.CodeModel
{
    public class ClassDefine
    {
        public string ClassName { get; set; }
        public string Remark { get; set; }
        public List<PropertyDefine> Properties { get; set; }
        public List<MethodDefine> Methods { get; set; }
        public ClassDefine()
        {
            this.Properties = new List<PropertyDefine>();
            this.Methods = new List<MethodDefine>();
        }
    }
}
