using CKGen.Base.Form;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKGen.Temp.AspnetForm
{
    public class PageFormModel
    {
        public Module FormModule { get; set; }

        public PageFormModel()
        {
            this.FormModule = new Module();
        }

        public PageFormModel(ITableInfo tbInfo)
        {
            this.FormModule = new Module(tbInfo);
        }
    }
}
