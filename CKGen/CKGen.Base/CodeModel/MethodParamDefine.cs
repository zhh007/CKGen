using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.CodeModel
{
    public class MethodParamDefine
    {
        public string ParamName { get; set; }
        public string ParamType { get; set; }
        public bool AllowNull { get; set; }
        public string Remark { get; set; }
    }
}
