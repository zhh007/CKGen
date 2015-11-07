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

    }
}
