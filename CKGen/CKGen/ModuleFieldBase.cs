using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen
{
    public class ModuleFieldBase
    {
        public Guid Id { get; private set; }

        public ModuleEntity Parent { get; private set; }

        public string Title { get; set; }

        public ModuleFieldBase(ModuleEntity parent, string title)
        {
            this.Id = Guid.NewGuid();
            this.Parent = parent;
            this.Title = title;
        }
    }
}
