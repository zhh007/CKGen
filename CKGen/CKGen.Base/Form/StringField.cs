using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Form
{
    public class StringField : ModuleField
    {
        public int? StringLength { get; set; }

        public StringField(Module parent, string title, string name, string codename)
            : base(parent, title, name, codename)
        {
        }
    }
}
