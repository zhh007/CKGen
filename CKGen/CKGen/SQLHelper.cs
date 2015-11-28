using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CKGen
{
    public class SQLHelper
    {
        public static void SetColumnDesc(string connStr, string schema, string tableName, string columnName, string desc)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES 
    WHERE [major_id] = OBJECT_ID(@TableName) AND [name] = N'MS_Description' 
        AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = @ColumnName AND [object_id] = OBJECT_ID(@TableName)))
EXECUTE sp_addextendedproperty N'MS_Description', @Desc, N'SCHEMA', @Schema, N'TABLE', @TableName, N'COLUMN', @ColumnName;
ELSE
EXECUTE sp_updateextendedproperty N'MS_Description', @Desc, N'SCHEMA', @Schema, N'TABLE', @TableName, N'COLUMN', @ColumnName;
";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter para_Schema = new SqlParameter("Schema", SqlDbType.NVarChar);
                para_Schema.Value = schema;
                cmd.Parameters.Add(para_Schema);

                SqlParameter para_TableName = new SqlParameter("TableName", SqlDbType.NVarChar);
                para_TableName.Value = tableName;
                cmd.Parameters.Add(para_TableName);

                SqlParameter para_ColumnName = new SqlParameter("ColumnName", SqlDbType.NVarChar);
                para_ColumnName.Value = columnName;
                cmd.Parameters.Add(para_ColumnName);

                SqlParameter para_Desc = new SqlParameter("Desc", SqlDbType.NVarChar);
                para_Desc.Value = desc;
                cmd.Parameters.Add(para_Desc);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void SetTableDesc(string connStr, string schema, string tableName, string desc)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES 
    WHERE [major_id] = OBJECT_ID(@TableName) AND [name] = N'MS_Description' AND [minor_id] = 0)
EXECUTE sp_addextendedproperty N'MS_Description', @Desc, N'SCHEMA', @Schema, N'TABLE', @TableName;
ELSE
EXECUTE sp_updateextendedproperty N'MS_Description', @Desc, N'SCHEMA', @Schema, N'TABLE', @TableName;
";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter para_Schema = new SqlParameter("Schema", SqlDbType.NVarChar);
                para_Schema.Value = schema;
                cmd.Parameters.Add(para_Schema);

                SqlParameter para_TableName = new SqlParameter("TableName", SqlDbType.NVarChar);
                para_TableName.Value = tableName;
                cmd.Parameters.Add(para_TableName);

                SqlParameter para_Desc = new SqlParameter("Desc", SqlDbType.NVarChar);
                para_Desc.Value = desc;
                cmd.Parameters.Add(para_Desc);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private static int NMax = 1073741823;
        private static int VMax = 2147483647;

        /// <summary>
        /// nvarchar(50)
        /// </summary>
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
