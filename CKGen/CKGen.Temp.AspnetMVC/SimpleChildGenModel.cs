using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.AspnetMVC
{
    public class SimpleChildGenModel
    {
        public ITableInfo ChildModel { get; set; }
        public ITableInfo ParentModel { get; set; }
        public string ChildCollectionName { get; set; }
        public List<InputItem> Items { get; set; }
        public string NameSpacePR { get; set; }
        public string WebProjNameSpace { get; set; }
        public SimpleChildGenModel()
        {
            this.Items = new List<InputItem>();
        }
    }

    public class InputItem
    {
        public string PascalName { get; set; }
        public string CamelName { get; }
        public string InputType { get; set; }
    }
}
