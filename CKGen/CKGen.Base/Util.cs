﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CKGen.DBSchema;
using System.Data.SqlClient;
using System.Data;

namespace CKGen
{
    public class Util
    {
        public static string SetValidName(string phrase)
        {
            string[] splittedPhrase = phrase.Split(' ', '-', '.');
            var sb = new StringBuilder();

            //sb = new StringBuilder();

            foreach (String s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                //if (splittedPhraseChars.Length > 0)
                //{
                //    splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                //}
                sb.Append(new String(splittedPhraseChars));
            }
            return sb.ToString();
        }

        /// <summary>
        /// columnName列名
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string SetCamelCase(string phrase)
        {
            string[] splittedPhrase = phrase.Split(' ', '-', '.');
            var sb = new StringBuilder();


            sb.Append(splittedPhrase[0].ToLower());
            splittedPhrase[0] = string.Empty;

            foreach (String s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                if (splittedPhraseChars.Length > 0)
                {
                    splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                }
                sb.Append(new String(splittedPhraseChars));
            }
            return sb.ToString();
        }

        /// <summary>
        /// ColumnName列名
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string SetPascalCase(string phrase)
        {
            string[] splittedPhrase = phrase.Split(' ', '-', '.');
            var sb = new StringBuilder();

            //sb = new StringBuilder();

            foreach (String s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                if (splittedPhraseChars.Length > 0)
                {
                    splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                }
                sb.Append(new String(splittedPhraseChars));
            }
            return sb.ToString();
        }

        public static string GetUserDefinedDataType(string userDefinedDataTypeName, string schema, string connStr)
        {
            string dbtype = "";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
SELECT name
FROM sys.types
WHERE user_type_id =
  (
   SELECT system_type_id
   FROM sys.types
     INNER JOIN sys.schemas
       ON sys.types.schema_id = sys.schemas.schema_id
   WHERE sys.schemas.name = @CurrentSchemaName
     AND sys.types.name = @UserDefinedDataTypeName
  )
";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter para1 = new SqlParameter("CurrentSchemaName", System.Data.SqlDbType.NVarChar);
                para1.Value = schema;
                cmd.Parameters.Add(para1);
                SqlParameter para2 = new SqlParameter("UserDefinedDataTypeName", System.Data.SqlDbType.NVarChar);
                para2.Value = userDefinedDataTypeName;
                cmd.Parameters.Add(para2);

                try
                {
                    conn.Open();
                    dbtype = cmd.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                        conn.Close();
                }
            }
            return dbtype;
        }

        ///// <summary>
        ///// 列描述
        ///// </summary>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //public static string GetColumnDesc(Column c)
        //{
        //    string desc = "";
        //    string sqlType = c.DataType.Name;
        //    if (c.ExtendedProperties["MS_Description"] != null)
        //        desc = c.ExtendedProperties["MS_Description"].ToString();
        //    if (c.InPrimaryKey)
        //    {
        //        if (desc.Length > 0)
        //            desc += "，主键";
        //        else
        //            desc = "主键";
        //    }
        //    int maxLen = c.DataType.MaximumLength;
        //    int maxPrecision = c.DataType.NumericPrecision;
        //    int numericScale = c.DataType.NumericScale;
        //    if (desc.Length > 0)
        //        desc += "，";

        //    if (LanguageConvert.IsStringType(sqlType))
        //    {
        //        desc += sqlType + "，长度：" + maxLen;
        //    }
        //    else if (LanguageConvert.IsNumericType(sqlType))
        //    {
        //        desc += sqlType + "，长度：" + maxLen + "，精度：" + maxPrecision + "，小数位数：" + numericScale;
        //    }

        //    return desc;
        //}

        ///// <summary>
        ///// 列描述
        ///// </summary>
        ///// <param name="c"></param>
        ///// <param name="full"></param>
        ///// <returns></returns>
        //public static string GetColumnDesc(Column c, bool full)
        //{
        //    if (full)
        //    {
        //        return GetColumnDesc(c);
        //    }

        //    string desc = "";
        //    string sqlType = c.DataType.Name;
        //    if (c.ExtendedProperties["MS_Description"] != null)
        //        desc = c.ExtendedProperties["MS_Description"].ToString();
        //    return desc;
        //}

        /// <summary>
        /// 生成insert语句,
        /// 计算列和自增列不插入
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetInsertSql(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nINSERT INTO [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");
            sbSql.Append(' ', 11);
            sbSql.Append("(");

            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                //计算列和自增列不插入
                if (col.Identity || col.Computed)
                    continue;

                sbSql.AppendFormat("[{0}]", col.RawName);
                if (i != len - 1)
                {
                    sbSql.Append("\r\n");
                    sbSql.Append(' ', 11);
                    sbSql.Append(",");
                }
            }

            sbSql.Append(")\r\n");
            sbSql.Append(' ', 5);
            sbSql.Append("VALUES\r\n");
            sbSql.Append(' ', 11);
            sbSql.Append("(");

            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                //计算列和自增列不插入
                if (col.Identity || col.Computed)
                    continue;

                sbSql.AppendFormat("@{0}", col.Name);
                if (i != len - 1)
                {
                    sbSql.Append("\r\n");
                    sbSql.Append(' ', 11);
                    sbSql.Append(",");
                }
            }

            sbSql.Append(")\r\n");

            return sbSql.ToString();
        }

        public static string GetSaveSql(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nIF NOT EXISTS (");

            sbSql.Append("\r\nSELECT * FROM [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");
            sbSql.Append(' ', 6);
            sbSql.Append("WHERE ");
            List<IColumnInfo> pks = GetPKs(table);
            int len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("[{0}] = @{1}", col.RawName, col.Name);
                if (i != len - 1)
                {
                    sbSql.Append(" AND ");
                }
            }
            sbSql.Append("\r\n");

            sbSql.Append(") \r\nBEGIN");

            //insert
            sbSql.Append(Util.GetInsertSql(table));

            sbSql.Append("END ELSE BEGIN");

            //update
            sbSql.Append(Util.GetUpdateByPKSql(table));

            sbSql.Append("END");

            return sbSql.ToString();
        }

        public static string GetSaveSqlForInfo(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nIF NOT EXISTS (");

            sbSql.Append("\r\nSELECT * FROM [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");
            sbSql.Append(' ', 6);
            sbSql.Append("WHERE ");
            List<IColumnInfo> pks = GetPKs(table);
            int len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("[{0}] = @{1}", col.RawName, col.Name);
                if (i != len - 1)
                {
                    sbSql.Append(" AND ");
                }
            }
            sbSql.Append("\r\n");

            sbSql.Append(") \r\nBEGIN");

            //insert
            sbSql.Append(Util.GetInsertSql(table));

            sbSql.Append("END ELSE BEGIN");

            //update
            sbSql.Append(Util.GetUpdateByPKSql(table));

            sbSql.Append("END");

            return sbSql.ToString();
        }

        ///// <summary>
        ///// new SqlParameter("Jb_xmmc", SqlDbType.NVarChar, 100)
        ///// </summary>
        ///// <param name="col"></param>
        ///// <returns></returns>
        //public static string GetNewSqlParameterStr(Column col)
        //{
        //    string ret = "";
        //    string sqlType = col.DataType.Name;
        //    string sqlDbType = DbTargetConvert.GetSqlDbType(sqlType);

        //    int colSize = 0;
        //    switch (sqlDbType)
        //    {
        //        case "SqlDbType.Int":
        //            colSize = 4;
        //            break;
        //        case "SqlDbType.Binary":
        //            colSize = 8;
        //            break;
        //        case "SqlDbType.UniqueIdentifier":
        //            colSize = 16;
        //            break;
        //        case "SqlDbType.Char":
        //        case "SqlDbType.NChar":
        //        case "SqlDbType.VarChar":
        //        case "SqlDbType.NVarChar":
        //            colSize = col.DataType.MaximumLength;
        //            break;
        //        default:
        //            colSize = 0;
        //            break;
        //    }

        //    ret = "new SqlParameter(\"";
        //    ret += Util.SetPascalCase(col.Name);
        //    ret += "\", ";
        //    ret += sqlDbType;

        //    if (colSize != 0)
        //        ret += ", " + colSize.ToString();

        //    ret += ")";

        //    return ret;
        //}

        /// <summary>
        /// (object)info.CreatedOn ?? DBNull.Value
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string SetParameterValueStr(IColumnInfo col, string objName)
        {
            string ret = "";

            if (col.Nullable)
            {
                ret = "(object)" + objName + "." + col.PascalName + " ?? DBNull.Value";
            }
            else
            {
                ret = objName + "." + col.PascalName;
            }

            return ret;
        }

        /// <summary>
        /// 未使用
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetUpdateByPKSql(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nUPDATE [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.Name);
            sbSql.Append("]\r\n");
            sbSql.Append(' ', 3);
            sbSql.Append("SET ");

            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                if (col.Identity || col.Computed || col.IsPrimaryKey)
                    continue;

                sbSql.AppendFormat("[{0}] = @{1}", col.Name, SetPascalCase(col.Name));
                if (i != len - 1)
                {
                    sbSql.Append("\r\n");
                    sbSql.Append(' ', 6);
                    sbSql.Append(",");
                }
            }

            sbSql.Append("\r\n");
            sbSql.Append(" WHERE ");

            List<IColumnInfo> pks = GetPKs(table);
            len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("[{0}] = @{1}", col.Name, SetPascalCase(col.Name));
                if (i != len - 1)
                {
                    sbSql.Append(" AND ");
                }
            }
            sbSql.Append("\r\n");

            return sbSql.ToString();
        }

        /// <summary>
        /// 生产Update脚本
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetUpdateByPKSql_2(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nUPDATE [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");
            sbSql.Append(' ', 3);
            sbSql.Append("SET ");

            sbSql.Append("{0}");

            sbSql.Append("\r\n");
            sbSql.Append(" WHERE ");

            List<IColumnInfo> pks = GetPKs(table);
            int len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("[{0}] = @{1}", col.RawName, col.Name);
                if (i != len - 1)
                {
                    sbSql.Append(" AND ");
                }
            }
            sbSql.Append("\r\n");

            return sbSql.ToString();
        }

        public static List<IColumnInfo> GetPKs(ITableInfo table)
        {
            List<IColumnInfo> result = new List<IColumnInfo>();
            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                if (col.IsPrimaryKey)
                    result.Add(col);
            }
            return result;
        }

        //public static string GetPKName(Table table)
        //{
        //    List<Column> result = new List<Column>();
        //    int len = table.Columns.Count;
        //    for (int i = 0; i < len; i++)
        //    {
        //        Column col = table.Columns[i];
        //        if (col.InPrimaryKey)
        //            return col.Name;
        //    }
        //    return "";
        //}

        public static string GetDefaultOrderby(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            List<IColumnInfo> pks = GetPKs(table);
            int len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("{0} asc", col.RawName);
                if (i != len - 1)
                {
                    sbSql.Append(", ");
                }
            }

            return sbSql.ToString();
        }

        public static string GetDeleteByPKSql(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nDELETE FROM [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");
            sbSql.Append(' ', 6);
            sbSql.Append("WHERE ");

            List<IColumnInfo> pks = GetPKs(table);
            int len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("[{0}] = @{1}", col.RawName, col.Name);
                if (i != len - 1)
                {
                    sbSql.Append(" AND ");
                }
            }
            sbSql.Append("\r\n");

            return sbSql.ToString();
        }

        public static string GetExistByPKSql(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nSELECT COUNT(*) FROM [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");
            sbSql.Append(' ', 6);
            sbSql.Append("WHERE ");

            List<IColumnInfo> pks = GetPKs(table);
            int len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("[{0}] = @{1}", col.RawName, col.Name);
                if (i != len - 1)
                {
                    sbSql.Append(" AND ");
                }
            }
            sbSql.Append("\r\n");

            return sbSql.ToString();
        }

        public static string GetPKDefine(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            List<IColumnInfo> pks = GetPKs(table);
            int len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("{0} {1}", col.CSharpType, col.CamelName);
                if (i != len - 1)
                {
                    sbSql.Append(", ");
                }
            }

            return sbSql.ToString();
        }

        public static string GetPKValueString(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            List<IColumnInfo> pks = GetPKs(table);
            int len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("this.{0}", col.PascalName);
                if (i != len - 1)
                {
                    sbSql.Append(", ");
                }
            }

            return sbSql.ToString();
        }

        public static string BuildGetByPKSql(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nSELECT ");// [");

            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                //if (col.InPrimaryKey)
                //    continue;

                sbSql.AppendFormat("[{0}]", col.RawName);
                if (i != len - 1)
                {
                    sbSql.Append("\r\n");
                    sbSql.Append(' ', 6);
                    sbSql.Append(",");
                }
            }
            sbSql.Append("\r\n");
            sbSql.Append(' ', 2);
            sbSql.Append("FROM [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");
            sbSql.Append(" WHERE ");

            List<IColumnInfo> pks = GetPKs(table);
            len = pks.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = pks[i];

                sbSql.AppendFormat("[{0}] = @{1}", col.RawName, col.Name);
                if (i != len - 1)
                {
                    sbSql.Append(" AND ");
                }
            }
            sbSql.Append("\r\n");

            return sbSql.ToString();
        }

        public static string BuildGetByWhere(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nSELECT ");// [");

            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                //if (col.InPrimaryKey)
                //    continue;

                sbSql.AppendFormat("[{0}]", col.RawName);
                if (i != len - 1)
                {
                    sbSql.Append("\r\n");
                    sbSql.Append(' ', 6);
                    sbSql.Append(",");
                }
            }
            sbSql.Append("\r\n");
            sbSql.Append(' ', 2);
            sbSql.Append("FROM [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");

            return sbSql.ToString();
        }

        public static string BuildSQL_TopOneByWhere(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nSELECT ");// [");

            sbSql.Append("TOP 1 ");

            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                //if (col.InPrimaryKey)
                //    continue;

                sbSql.AppendFormat("[{0}]", col.RawName);
                if (i != len - 1)
                {
                    sbSql.Append("\r\n");
                    sbSql.Append(' ', 6);
                    sbSql.Append(",");
                }
            }
            sbSql.Append("\r\n");
            sbSql.Append(' ', 2);
            sbSql.Append("FROM [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");

            return sbSql.ToString();
        }

        public static string BuildSQL_TopNByWhere(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("\r\nSELECT ");// [");

            sbSql.Append("TOP {0} ");

            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                sbSql.AppendFormat("[{0}]", col.RawName);
                if (i != len - 1)
                {
                    sbSql.Append("\r\n");
                    sbSql.Append(' ', 6);
                    sbSql.Append(",");
                }
            }
            sbSql.Append("\r\n");
            sbSql.Append(' ', 2);
            sbSql.Append("FROM [");
            sbSql.Append(table.Schema);
            sbSql.Append("].[");
            sbSql.Append(table.RawName);
            sbSql.Append("]\r\n");

            return sbSql.ToString();
        }

        public static string BuildSQL_Paged(ITableInfo table)
        {
            string orderbyStr = string.Format("{0} DESC", GetDefaultColumn(table));
            string whereStr = "";
            string sql = string.Format(@"
SELECT * FROM (
    SELECT *
        , (ROW_NUMBER() OVER (ORDER BY {0})) AS RowNumber
        , (((ROW_NUMBER() OVER (ORDER BY {0})) - 1)/@PageSize) + 1 AS PageNumber
    FROM [{1}] {2}
) as t
WHERE PageNumber = @PageIndex

SELECT @TotalCount = COUNT(*) FROM (
    SELECT *
        , (ROW_NUMBER() OVER (ORDER BY {0})) AS RowNumber
        , (((ROW_NUMBER() OVER (ORDER BY {0})) - 1)/@PageSize) + 1 AS PageNumber
    FROM [{1}] {2}
) as t", orderbyStr, table.RawName, whereStr);

            return sql;
        }

        public static string GetDefaultColumn(ITableInfo table)
        {
            var col = (from p in table.Columns
                       where p.IsPrimaryKey && p.Identity
                       select p).FirstOrDefault();
            if(col != null)
            {
                return "[" + col.RawName + "]";
            }

            col = (from p in table.Columns
                   where p.CSharpType == "DateTime" && (p.LowerName.Contains("update") && p.LowerName.Contains("modif"))
                   select p).FirstOrDefault();
            if (col != null)
            {
                return "[" + col.RawName + "]";
            }

            col = (from p in table.Columns
                   where p.CSharpType == "DateTime" && (p.LowerName.Contains("create") && p.LowerName.Contains("add"))
                   select p).FirstOrDefault();
            if (col != null)
            {
                return "[" + col.RawName + "]";
            }

            return "[" + table.Columns[0].RawName + "]";
        }

        public static string GetColumnString(ITableInfo table)
        {
            StringBuilder sbSql = new StringBuilder();

            //sbSql.Append("\r\nSELECT ");// [");

            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                //if (col.InPrimaryKey)
                //    continue;

                sbSql.AppendFormat("[{0}]", col.RawName);
                if (i != len - 1)
                {
                    //sbSql.Append("\r\n");
                    //sbSql.Append(' ', 6);
                    sbSql.Append(", ");
                }
            }
            //sbSql.Append("\r\n");
            //sbSql.Append(' ', 2);
            //sbSql.Append("FROM [");
            //sbSql.Append(table.Schema);
            //sbSql.Append("].[");
            //sbSql.Append(table.RawName);
            //sbSql.Append("]\r\n");

            return sbSql.ToString();
        }

        /// <summary>
        /// default(int)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string BuildGetSqlReaderNullStr(IColumnInfo column)
        {
            string sqlType = column.DBType;
            string sqlDbType = column.SqlDataType;
            string languageType = column.CSharpType;
            StringBuilder sb = new StringBuilder();

            //如果该列是可空，并且是值类型
            if (column.Nullable && LanguageConvert.IsValueType(languageType))
            {
                sb.Append("null");
            }
            else
            {
                sb.AppendFormat("default({0})", languageType);
            }

            return sb.ToString();
        }

        /// <summary>
        /// info.PathId = sdr.GetGuid(pathidOrdinal);
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string BuildSetFieldValue(IColumnInfo column)
        {
            string tmp = "entity.{0} = {1};";
            string result = "";

            string sqlType = column.DBType;
            string sqlDbType = DbTargetConvert.GetSqlDbType(sqlType);

            if (sqlDbType == "SqlDbType.Image" || sqlDbType == "SqlDbType.Binary"
                || sqlDbType == "SqlDbType.VarBinary" || sqlDbType == "SqlDbType.Timestamp")
            {
                string t = @"
long blobSize = sdr.GetBytes({0}Ordinal, 0, null, 0, 0);
byte[] buffer = new byte[blobSize];
long currPos = 0;
while (currPos < blobSize)
{{
    currPos += sdr.GetBytes({0}Ordinal, currPos, buffer, 0, 1024);
}}
info.{1} = buffer;
";
                result = string.Format(t, column.CamelName, column.PascalName);
            }
            else
            {
                result = string.Format(tmp, column.PascalName, BuildGetSqlReaderValueStr(column));
            }

            return result;
        }

        /// <summary>
        /// sdr.GetString(parentguidOrdinal)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string BuildGetSqlReaderValueStr(IColumnInfo column)
        {
            string sqlType = column.DBType;
            string sqlDbType = DbTargetConvert.GetSqlDbType(sqlType);
            string languageType = LanguageConvert.GetCSharpTypeFromMSSQL(sqlType);
            StringBuilder sb = new StringBuilder();

            string sdrGetMenth = string.Empty;
            switch (sqlDbType)
            {
                case "SqlDbType.BigInt":
                    sdrGetMenth = "GetInt64";
                    break;
                case "SqlDbType.Decimal":
                    sdrGetMenth = "GetDecimal";
                    break;
                case "SqlDbType.Float":
                    sdrGetMenth = "GetDouble";
                    break;
                case "SqlDbType.Int":
                    sdrGetMenth = "GetInt32";
                    break;
                case "SqlDbType.Money":
                    sdrGetMenth = "GetDecimal";
                    break;
                case "SqlDbType.Real":
                    sdrGetMenth = "GetFloat";
                    break;
                case "SqlDbType.SmallInt":
                    sdrGetMenth = "GetInt16";
                    break;
                case "SqlDbType.SmallMoney":
                    sdrGetMenth = "GetDecimal";
                    break;
                case "SqlDbType.TinyInt":
                    sdrGetMenth = "GetByte";
                    break;
                case "SqlDbType.Binary":
                    sdrGetMenth = "GetBytes";
                    break;
                case "SqlDbType.Bit":
                    sdrGetMenth = "GetBoolean";
                    break;
                case "SqlDbType.Date":
                case "SqlDbType.DateTime":
                case "SqlDbType.DateTime2":
                case "SqlDbType.SmallDateTime":
                    sdrGetMenth = "GetDateTime";
                    break;
                case "SqlDbType.Image":
                    sdrGetMenth = "GetBytes";
                    break;
                case "SqlDbType.NChar":
                case "SqlDbType.NText":
                case "SqlDbType.NVarChar":
                case "SqlDbType.Char":
                case "SqlDbType.Text":
                case "SqlDbType.VarChar":
                    sdrGetMenth = "GetString";
                    break;
                case "SqlDbType.Timestamp":
                    sdrGetMenth = "GetBytes";
                    break;
                case "SqlDbType.UniqueIdentifier":
                    sdrGetMenth = "GetGuid";
                    break;
                case "SqlDbType.VarBinary":
                    sdrGetMenth = "GetBytes";
                    break;
                case "SqlDbType.Xml":
                    sdrGetMenth = "GetString";
                    break;
                case "SqlDbType.Variant":
                    sdrGetMenth = "GetValue";
                    break;
                default:
                    break;
            }

            sb.AppendFormat("sdr.{0}({1}Ordinal)", sdrGetMenth, column.CamelName);

            return sb.ToString();
        }

        /// <summary>
        /// if(item.Key == "Feature" || item.Key == "Feature2") continue;
        /// 计算列，自增列，主键列
        /// </summary>
        /// <returns></returns>
        public static string BuildGetKeyEqContinue(ITableInfo table)
        {
            string result = "";
            int len = table.Columns.Count;
            for (int i = 0; i < len; i++)
            {
                IColumnInfo col = table.Columns[i];
                if (col.IsPrimaryKey || col.Computed || col.Identity)
                {
                    if (result != "")
                    {
                        result += " || ";
                    }
                    result += "item.Key == \"" + col.RawName + "\"";
                }
            }

            if (result != "")
            {
                result = string.Format("if({0}) continue;", result);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string GetSqlParameter(IColumnInfo col)
        {
            string result = "";
            int colSize = 0;
            switch (col.SqlDataType)
            {
                case "SqlDbType.Int":
                    colSize = 4;
                    break;
                case "SqlDbType.Binary":
                    colSize = 8;
                    break;
                case "SqlDbType.UniqueIdentifier"://Guid
                    colSize = 16;
                    break;
                case "SqlDbType.Char":
                case "SqlDbType.NChar":
                case "SqlDbType.VarChar":
                case "SqlDbType.NVarChar":
                    colSize = col.MaxLength.GetValueOrDefault();
                    break;
                default:
                    colSize = 0;
                    break;
            }

            if (colSize != 0)
            {
                result = string.Format("new SqlParameter(\"{0}\", {1}, {2})", col.Name, col.SqlDataType, colSize);
            }
            else
            {
                result = string.Format("new SqlParameter(\"{0}\", {1})", col.Name, col.SqlDataType);
            }

            return result;
        }

        private static int NMax = 1073741823;
        private static int VMax = 2147483647;

        public static string GetFullSqlType(IColumnInfo col)
        {
            string result = "";

            switch (col.DBType)
            {
                case "bigint":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "binary"://binary(50)
                    result = string.Format("{0}({1})", col.DBType, col.MaxLength);
                    break;
                case "bit":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "char"://char(10)"
                    result = string.Format("{0}({1})", col.DBType, col.MaxLength);
                    break;
                case "date":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "datetime":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "datetime2"://datetime2(7)
                    result = string.Format("{0}({1})", col.DBType, col.Scale);
                    break;
                case "datetimeoffset"://datetimeoffset(7)
                    result = string.Format("{0}({1})", col.DBType, col.Scale);
                    break;
                case "decimal"://decimal(18, 0)
                    result = string.Format("{0}({1}, {2})", col.DBType, col.Precision, col.Scale);
                    break;
                case "float":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "geography":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "geometry":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "hierarchyid":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "image":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "int":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "money":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "nchar"://nchar(10)
                    result = string.Format("{0}({1})", col.DBType, col.MaxLength);
                    break;
                case "ntext":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "numeric"://numeric(18, 0)
                    result = string.Format("{0}({1}, {2})", col.DBType, col.Precision, col.Scale);
                    break;
                case "nvarchar"://nvarchar(50)
                    if (col.MaxLength == NMax)
                    {
                        result = string.Format("{0}(MAX)", col.DBType);
                    }
                    else
                    {
                        result = string.Format("{0}({1})", col.DBType, col.MaxLength);
                    }
                    break;
                //case "nvarchar(MAX)":
                //    break;
                case "real":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "smalldatetime":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "smallint":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "smallmoney":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "sql_variant":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "text":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "time"://time(7)
                    result = string.Format("{0}({1})", col.DBType, col.Scale);
                    break;
                case "timestamp":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "tinyint":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "uniqueidentifier":
                    result = string.Format("{0}", col.DBType);
                    break;
                case "varbinary"://varbinary(50)
                    if (col.MaxLength == VMax)
                    {
                        result = string.Format("{0}(MAX)", col.DBType);
                    }
                    else
                    {
                        result = string.Format("{0}({1})", col.DBType, col.MaxLength);
                    }
                    break;
                //case "varbinary(MAX)":
                //    break;
                case "varchar"://varchar(50)
                    if (col.MaxLength == VMax)
                    {
                        result = string.Format("{0}(MAX)", col.DBType);
                    }
                    else
                    {
                        result = string.Format("{0}({1})", col.DBType, col.MaxLength);
                    }
                    break;
                //case "varchar(MAX)":
                //    break;
                case "xml":
                    result = string.Format("{0}", col.DBType);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}