//// -----------------------------------------------------------------------
//// <copyright file="SQLSchemaLoader.cs" company="Microsoft">
//// TODO: Update copyright text.
//// </copyright>
//// -----------------------------------------------------------------------

//namespace DevScaffold.Library.Database
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using System.Data.SqlClient;
//    using System.Data;

//    /// <summary>
//    /// TODO: Update summary.
//    /// </summary>
//    public class SQLSchemaLoader
//    {
//        private string connectionString;
//        private string connectionStringTemplete;
//        private string[] ignoreDatabaseName = new string[] { "master", "model", "msdb", "tempdb" };

//        public SQLSchemaLoader(string connstr)
//        {
//            connectionString = connstr;
//            if (connstr.IndexOf("Database", StringComparison.CurrentCultureIgnoreCase) != -1)
//            {
//                int pos = connstr.IndexOf("Database");
//                int pos2 = connstr.IndexOf(";", pos + 8);
//                pos = connstr.IndexOf("=", pos + 8);

//                connectionStringTemplete = connstr.Remove(pos, pos2 - pos);
//                connectionStringTemplete = connectionStringTemplete.Insert(pos, "={0}");
//            }
//        }

//        public List<DatabaseInfo> Load()
//        {
//            List<DatabaseInfo> dbs = new List<DatabaseInfo>();

//            using (SqlConnection connection = new SqlConnection(this.connectionString))
//            {
//                connection.Open();
//                DataTable dbTable = connection.GetSchema("Databases");
//                if (dbTable != null && dbTable.Rows.Count > 0)
//                {
//                    for (int i = 0; i < dbTable.Rows.Count; i++)
//                    {
//                        string dbname = dbTable.Rows[i][0].ToString();
//                        if (Array.IndexOf(this.ignoreDatabaseName, dbname) != -1)
//                            continue;
//                        DatabaseInfo dbInfo = new DatabaseInfo();
//                        dbInfo.Name = dbname;
//                        dbInfo.Dbid = Convert.ToInt32(dbTable.Rows[i][1]);
//                        dbInfo.Createtime = Convert.ToDateTime(dbTable.Rows[i][2]);
//                        dbs.Add(dbInfo);
//                    }
//                }
//            }

//            for (int i = 0; i < dbs.Count; i++)
//            {
//                DatabaseInfo dbinfo = dbs[i];
//                string connStr = string.Format(this.connectionStringTemplete, dbinfo.Name);
//                using (SqlConnection connection = new SqlConnection(connStr))
//                {
//                    connection.Open();
//                    DataTable ttable = connection.GetSchema("Tables");
//                    DataTable columnTable = connection.GetSchema("Columns");
//                    //DataTable kTable = GetPKs(dbinfo.Name);//PrimaryKeys
//                    if (ttable == null)
//                        continue;
//                    if (ttable.Rows.Count == 0)
//                        continue;
//                    if (columnTable == null)
//                        continue;
//                    if (columnTable.Rows.Count == 0)
//                        continue;

//                    DataRow[] rows = ttable.Select("table_catalog = '" + dbinfo.Name + "'");
//                    foreach (var tableItem in rows)
//                    {
//                        string name = tableItem["table_name"].ToString();
//                        string schema = tableItem["table_schema"].ToString();
//                        string type = tableItem["table_type"].ToString();
//                        List<ColumnInfo> columnList = new List<ColumnInfo>();
//                        DataRow[] columns = columnTable.Select("table_name = '" + name + "'", "ordinal_position asc");
//                        foreach (var colItem in columns)
//                        {
//                            ColumnInfo cinfo = new ColumnInfo();
//                            cinfo.Name = colItem["column_name"].ToString();
//                            cinfo.OrderNum = Convert.ToInt32(colItem["ordinal_position"]);
//                            cinfo.DefaultValue = colItem["column_default"].ToString();
//                            cinfo.IsNullable = colItem["is_nullable"].ToString().ToLower() == "yes";
//                            cinfo.DbDatatype = colItem["data_type"].ToString();
//                            cinfo.Maxlength = (colItem["character_maximum_length"] != DBNull.Value) ? Convert.ToInt32(colItem["character_maximum_length"]) : -1;
//                            cinfo.Precision = (colItem["numeric_precision"] != DBNull.Value) ? Convert.ToInt32(colItem["numeric_precision"]) : -1;
//                            cinfo.Scale = (colItem["numeric_scale"] != DBNull.Value) ? Convert.ToInt32(colItem["numeric_scale"]) : -1;
//                            columnList.Add(cinfo);
//                        }

//                        if (type == "BASE TABLE")
//                        {
//                            TableInfo tinfo = new TableInfo();
//                            tinfo.Schema = schema;
//                            tinfo.Name = name;
//                            tinfo.Columns.AddRange(columnList);
//                            dbinfo.Tables.Add(tinfo);
//                        }
//                        else//(type == "VIEW")
//                        {
//                            ViewInfo vinfo = new ViewInfo();
//                            vinfo.Schema = schema;
//                            vinfo.Name = name;
//                            vinfo.Columns.AddRange(columnList);
//                            dbinfo.Views.Add(vinfo);
//                        }
//                    }
//                }
//            }

//            return dbs;
//        }

//        public DataTable GetPKs(string dbname)
//        {
//            DataTable dt = null;
//            using (SqlConnection connection = new SqlConnection(this.connectionString))
//            {
//                connection.Open();
//                string sql = @"
//USE {0};
//select 	pk.TABLE_NAME, c.COLUMN_NAME 
//from 	INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,
//		INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
//where 	pk.TABLE_CATALOG = @dbname
//	and	CONSTRAINT_TYPE = 'PRIMARY KEY'
//	and	c.TABLE_NAME = pk.TABLE_NAME
//	and	c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME
//";
//                sql = string.Format(sql, dbname);
//                SqlCommand cmd = new SqlCommand(sql, connection);
//                SqlParameter para1 = new SqlParameter("dbname", dbname);
//                cmd.Parameters.Add(para1);

//                SqlDataAdapter sda = new SqlDataAdapter(cmd);
//                DataSet ds = new DataSet();
//                sda.Fill(ds);
//                if (ds != null && ds.Tables.Count > 0)
//                {
//                    dt = ds.Tables[0];
//                }
//            }

//            return dt;
//        }
//    }
//}
