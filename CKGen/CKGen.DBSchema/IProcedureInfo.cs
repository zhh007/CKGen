using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.DBSchema
{
    public interface IProcedureInfo
    {
        string Name { get; set; }

        string Schema { get; set; }

        IDatabaseInfo Database { get; }
    }
}
