using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

/*
<database name="">
    <table rawname="" schema="" desc="">
        <column rawname="" dbtype="" dbtargettype="" languagetype="" desc="" nullable="" iskey="" maxlength="" precision="" scale="" identity="" computed="" />
    </table>
</database>
*/
namespace CKGen
{
    public class DatabaseSchemaSetting
    {
        private static string GetStorePath(IDatabaseInfo db)
        {
            return Path.Combine(Environment.CurrentDirectory, "Settings\\" + "db_" + SystemConfig.DBName + ".xml");
        }

        #region 将新数据结构同步到本地

        /// <summary>
        /// 将新数据结构同步到本地
        /// </summary>
        public static IDatabaseInfo SyncToLocal(IDatabaseInfo db)
        {
            XElement root = null;
            string filePath = GetStorePath(db);
            if (File.Exists(filePath))
            {
                string txt = File.ReadAllText(filePath);
                root = XElement.Parse(txt);
            }

            if (root == null)
            {
                root = new XElement("database");
            }

            UpdateTables(root, db.Tables);

            XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xdoc.Save(filePath);

            return db;
        }

        private static void UpdateTables(XElement root, List<ITableInfo> tables)
        {
            List<XElement> allXmlTable = new List<XElement>();
            foreach (ITableInfo tableInfo in tables)
            {
                var tableNode = (from p in root.Elements()
                                 where p.Attribute("rawname").Value == tableInfo.RawName
                                 select p).FirstOrDefault();
                if (tableNode != null)
                {
                    string db_desc = tableInfo.Description;
                    string local_desc = tableNode.Attribute("desc") != null ? tableNode.Attribute("desc").Value : "";
                    string new_desc = "";

                    if (local_desc != db_desc)
                    {
                        new_desc = db_desc;
                    }

                    tableInfo.Attributes["local_desc"] = local_desc;
                    tableInfo.Attributes["new_desc"] = new_desc;
                }
                else
                {//new table
                    tableInfo.Attributes["local_desc"] = tableInfo.Description;
                    tableInfo.Attributes["new_desc"] = "";

                    tableNode = new XElement("table");
                    tableNode.Add(new XAttribute("rawname", tableInfo.RawName));
                    tableNode.Add(new XAttribute("schema", tableInfo.Schema));
                    tableNode.Add(new XAttribute("desc", tableInfo.Description));
                }

                UpdateTableColumns(tableNode, tableInfo);

                allXmlTable.Add(tableNode);
            }

            root.RemoveNodes();
            foreach (var tb in allXmlTable)
            {
                root.Add(tb);
            }
        }

        private static void UpdateTableColumns(XElement tableNode, ITableInfo tableInfo)
        {
            List<XElement> allXMLColumns = new List<XElement>();
            foreach (IColumnInfo col in tableInfo.Columns)
            {
                var colNode = (from c in tableNode.Elements()
                               where c.Attribute("rawname").Value == col.RawName
                               select c).FirstOrDefault();
                if (colNode != null)
                {
                    string db_desc = col.Description;
                    string local_desc = colNode.Attribute("desc").Value;
                    string new_desc = "";
                    if (local_desc != db_desc)
                    {
                        new_desc = db_desc;
                    }
                    col.Attributes["local_desc"] = local_desc;
                    col.Attributes["new_desc"] = new_desc;
                }
                else
                {
                    col.Attributes["local_desc"] = col.Description;
                    col.Attributes["new_desc"] = "";
                    colNode = new XElement("column");
                    colNode.SetAttributeValue("desc", col.Description);
                }

                colNode.SetAttributeValue("rawname", col.RawName);
                colNode.SetAttributeValue("dbtype", col.DBType);
                colNode.SetAttributeValue("dbtargettype", col.DbTargetType);
                colNode.SetAttributeValue("languagetype", col.LanguageType);
                colNode.SetAttributeValue("nullable", col.Nullable);
                colNode.SetAttributeValue("iskey", col.IsPrimaryKey);
                colNode.SetAttributeValue("maxlength", col.MaxLength);
                colNode.SetAttributeValue("precision", col.Precision);
                colNode.SetAttributeValue("scale", col.Scale);
                colNode.SetAttributeValue("identity", col.Identity);
                colNode.SetAttributeValue("computed", col.Computed);

                allXMLColumns.Add(colNode);
            }

            tableNode.RemoveNodes();
            foreach (var col in allXMLColumns)
            {
                tableNode.Add(col);
            }
        }

        #endregion

        /// <summary>
        /// 保存说明
        /// </summary>
        public static void SaveDesc(IDatabaseInfo db)
        {
            string filePath = GetStorePath(db);

            //save
            XElement root = new XElement("database");

            foreach (ITableInfo tb in db.Tables)
            {
                XElement elTable = new XElement("table");
                elTable.Add(new XAttribute("rawname", tb.RawName));
                elTable.Add(new XAttribute("schema", tb.Schema));

                string table_desc = tb.Description;
                if (!string.IsNullOrEmpty(tb.Attributes["new_desc"]))
                {
                    table_desc = tb.Attributes["new_desc"];
                    tb.Description = table_desc;
                    tb.Attributes["local_desc"] = table_desc;
                    tb.Attributes["new_desc"] = "";

                    SQLHelper.SetTableDesc(SystemConfig.DBLink.ConnectionStringForExecute, tb.Schema, tb.RawName, table_desc);
                }
                elTable.Add(new XAttribute("desc", table_desc));

                foreach (IColumnInfo col in tb.Columns)
                {
                    string desc = col.Description;

                    if (!string.IsNullOrEmpty(col.Attributes["new_desc"]))
                    {
                        desc = col.Attributes["new_desc"];
                        col.Description = desc;
                        col.Attributes["local_desc"] = desc;
                        col.Attributes["new_desc"] = "";

                        SQLHelper.SetColumnDesc(SystemConfig.DBLink.ConnectionStringForExecute, tb.Schema, tb.RawName, col.RawName, desc);
                    }
                    XElement elColumn = new XElement("column");
                    elColumn.Add(new XAttribute("rawname", col.RawName));
                    elColumn.Add(new XAttribute("dbtype", col.DBType));
                    elColumn.Add(new XAttribute("dbtargettype", col.DbTargetType));
                    elColumn.Add(new XAttribute("languagetype", col.LanguageType));
                    elColumn.Add(new XAttribute("desc", desc));
                    elColumn.Add(new XAttribute("nullable", col.Nullable));
                    elColumn.Add(new XAttribute("iskey", col.IsPrimaryKey));
                    elColumn.Add(new XAttribute("maxlength", col.MaxLength));
                    elColumn.Add(new XAttribute("precision", col.Precision));
                    elColumn.Add(new XAttribute("scale", col.Scale));
                    elColumn.Add(new XAttribute("identity", col.Identity));
                    elColumn.Add(new XAttribute("computed", col.Computed));
                    elTable.Add(elColumn);
                }
                root.Add(elTable);
            }
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xdoc.Save(filePath);
        }
    }
}
