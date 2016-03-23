using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet.Snippet
{
    public class CountModel
    {
        public ITableInfo Table { get; set; }
        public List<IColumnInfo> ParamColumns { get; set; }

        public CountModel()
        {
            ParamColumns = new List<IColumnInfo>();
        }

        public string GetMethodParamString()
        {
            StringBuilder sb = new StringBuilder();
            if (ParamColumns != null && ParamColumns.Count > 0)
            {
                int len = ParamColumns.Count;
                for (int i = 0; i < len; i++)
                {
                    IColumnInfo col = ParamColumns[i];
                    sb.AppendFormat("{0} {1}", col.LanguageType, col.CamelName);
                    if (i != len - 1)
                    {
                        sb.Append(", ");
                    }
                }
            }
            return sb.ToString();
        }

        public string GetSQL()
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nSELECT COUNT(*) FROM [");
            sbSql.Append(Table.Schema);
            sbSql.Append("].[");
            sbSql.Append(Table.RawName);
            sbSql.Append("]\r\n");

            if (ParamColumns != null && ParamColumns.Count > 0)
            {
                sbSql.Append(' ', 6);
                sbSql.Append("WHERE ");
                int len = ParamColumns.Count; ;
                for (int i = 0; i < len; i++)
                {
                    IColumnInfo col = ParamColumns[i];
                    sbSql.AppendFormat("[{0}] = @{1}", col.RawName, col.Name);
                    if (i != len - 1)
                    {
                        sbSql.Append(" AND ");
                    }
                }
                sbSql.Append("\r\n");
            }

            return sbSql.ToString();
        }
    }
}
