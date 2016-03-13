using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.CodeModel
{
    public class PropertyDefine
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public bool AllowNull { get; set; }
        public string Remark { get; set; }
    }
}
