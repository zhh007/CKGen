using CKGen.Base.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.CodeModel
{
    public enum SQLQueryExecuteType
    {
        ReadRows,
        ReadOneRow,
        ExecuteSclor,
        ExecuteNoQuery
    }
    public class SQLQuerySetting
    {
        public SQLQueryExecuteType Type { get; set; }
        public string RowClassName { get; set; }
        public Module ModuleType { get; set; }
    }
}
