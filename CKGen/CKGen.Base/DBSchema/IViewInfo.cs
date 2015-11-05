using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBSchema
{
    public interface IViewInfo
    {
        string Schema { get; set; }

        string Name { get; set; }

        List<IColumnInfo> Columns { get; set; }

        IDatabaseInfo Database { get; }
    }
}
