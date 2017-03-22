using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.AspnetMVC2
{
    [Serializable]
    public class AspnetMVCSetting
    {
        public string Namespace { get; set; }
        public string DBContextTypeName { get; set; }
    }
}
