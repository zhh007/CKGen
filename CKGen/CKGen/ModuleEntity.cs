using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen
{
    public class ModuleEntity
    {
        public ITableInfo DatabaseTable { get; private set; }

        public List<ModuleField> Fields { get; private set; }

        public string ModuleName { get; set; }

        public string DisplayName { get; set; }

        public ModuleEntity(ITableInfo tbInfo)
        {
            this.DatabaseTable = tbInfo;

            var fields = new List<ModuleField>();
            foreach (var col in tbInfo.Columns)
            {
                ModuleField field = new ModuleField(this, col.Description, col.PascalName);
                fields.Add(field);
            }
            this.Fields = fields;
        }
    }
}
