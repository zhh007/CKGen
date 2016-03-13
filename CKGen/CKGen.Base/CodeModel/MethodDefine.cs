using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.CodeModel
{
    public class MethodDefine
    {
        public string MethodName { get; set; }
        public string Remark { get; set; }
        public List<ParamDefine> Params { get; set; }
        public string ReturnType { get; set; }
        public MethodDefine()
        {
            this.Params = new List<ParamDefine>();
        }
    }
}
