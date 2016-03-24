using CKGen.Base.CodeModel;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet.Snippet
{
    public class TablePagedModel
    {
        public ITableInfo Table { get; set; }
        public List<IColumnInfo> ParamColumns { get; set; }
        public List<OrderByItem> OrderBy { get; set; }
        public string ResultItemClassName { get; set; }

        public TablePagedModel()
        {
            ParamColumns = new List<IColumnInfo>();
            OrderBy = new List<OrderByItem>();
        }

        public string GetMethodParam()
        {
            StringBuilder sb = new StringBuilder();

            int len = ParamColumns.Count;
            for (int i = 0; i < len; i++)
            {
                if(i > 0)
                {
                    sb.Append(", ");
                }
                sb.AppendFormat("{0} {1}", ParamColumns[i].LanguageType, ParamColumns[i].CamelName);
            }
            if(len > 0)
            {
                sb.Append(", ");
            }

            return sb.ToString();
        }

        public string GetPagedSQL()
        {
            StringBuilder sb = new StringBuilder();
            int len = Table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = Table.Columns[i];

                sb.AppendFormat("[{0}]", col.RawName);
                if (i != len - 1)
                {
                    sb.Append("\r\n");
                    sb.Append(' ', 8);
                    sb.Append(",");
                }
            }

            string orderbyStr = "ORDER BY";
            for (int i = 0; i < OrderBy.Count; i++)
            {
                if(i > 0)
                {
                    orderbyStr += ",";
                }
                orderbyStr += string.Format(" [{0}] {1}", OrderBy[i].ColumnName, OrderBy[i].Direction == OrderDirection.Asc ? "ASC" : "DESC");
            }

            string whereStr = "WHERE";
            for (int i = 0; i < ParamColumns.Count; i++)
            {
                if(i > 0)
                {
                    whereStr += " AND";
                }
                whereStr += string.Format(" [{0}] = @{1}", ParamColumns[i].RawName, ParamColumns[i].Name);
            }

            string sql = string.Format(@"
SELECT * FROM (
    SELECT {3}
        ,(ROW_NUMBER() OVER ({0})) AS RowNumber
    FROM [{1}] {2}
) as t
WHERE PageNumber BETWEEN @Begin AND @End

SELECT @Total = COUNT(*) FROM [{1}] {2}
", orderbyStr, Table.RawName, whereStr, sb.ToString());

            return sql;
        }
    }
}
