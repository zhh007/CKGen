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
        <column rawname="" dbtype="" dbtargettype="" csharptype="" desc="" nullable="" iskey="" maxlength="" precision="" scale="" identity="" computed="" />
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
            string filePath = GetStorePath(db);
            if (File.Exists(filePath))
            {
                Update(filePath, db);
            }
            else
            {
                Create(db, filePath);
            }

            return db;
        }

        private static void Update(string filePath, IDatabaseInfo db)
        {
            string txt = File.ReadAllText(filePath);
            XElement root = XElement.Parse(txt);

            List<XElement> allXmlTable = new List<XElement>();
            foreach (ITableInfo tableInfo in db.Tables)
            {
                var xmlTable = (from p in root.Elements()
                                where p.Attribute("rawname").Value == tableInfo.RawName
                                select p).FirstOrDefault();
                if (xmlTable != null)
                {
                    string db_desc = tableInfo.Description;
                    string local_desc = xmlTable.Attribute("desc") != null ? xmlTable.Attribute("desc").Value : "";
                    string new_desc = "";

                    if (local_desc != db_desc)
                    {
                        new_desc = db_desc;
                    }

                    tableInfo.Attributes["local_desc"] = local_desc;
                    tableInfo.Attributes["new_desc"] = new_desc;

                    List<XElement> allXMLColumns = new List<XElement>();
                    foreach (IColumnInfo columnInfo in tableInfo.Columns)
                    {
                        updateForColumn(xmlTable, allXMLColumns, columnInfo);
                    }

                    xmlTable.RemoveNodes();
                    foreach (var col in allXMLColumns)
                    {
                        xmlTable.Add(col);
                    }
                }
                else
                {//new table
                    xmlTable = createXmlTable(tableInfo);
                }

                allXmlTable.Add(xmlTable);
            }

            root.RemoveNodes();
            foreach (var tb in allXmlTable)
            {
                root.Add(tb);
            }

            XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xdoc.Save(filePath);
        }

        private static void updateForColumn(XElement xmlTable, List<XElement> allXMLColumns, IColumnInfo columnInfo)
        {
            var colEl = (from c in xmlTable.Elements()
                         where c.Attribute("rawname").Value == columnInfo.RawName
                         select c).FirstOrDefault();
            if (colEl != null)
            {
                string db_desc = columnInfo.Description;
                string local_desc = colEl.Attribute("desc").Value;
                string new_desc = "";

                if (local_desc != db_desc)
                {
                    new_desc = db_desc;
                }

                updateXmlColumn(colEl, columnInfo);

                columnInfo.Attributes["local_desc"] = local_desc;
                columnInfo.Attributes["new_desc"] = new_desc;
            }
            else
            {
                colEl = createXmlColumn(columnInfo);
            }
            allXMLColumns.Add(colEl);
        }

        public static void Create(IDatabaseInfo db, string filePath)
        {
            XElement root = new XElement("database");

            foreach (ITableInfo tb in db.Tables)
            {
                XElement elTable = createXmlTable(tb);
                root.Add(elTable);
            }
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xdoc.Save(filePath);
        }

        private static XElement createXmlTable(ITableInfo tb)
        {
            tb.Attributes["local_desc"] = tb.Description;
            tb.Attributes["new_desc"] = "";

            XElement elTable = new XElement("table");
            elTable.Add(new XAttribute("rawname", tb.RawName));
            elTable.Add(new XAttribute("schema", tb.Schema));
            elTable.Add(new XAttribute("desc", tb.Description));

            foreach (IColumnInfo col in tb.Columns)
            {
                var elColumn = createXmlColumn(col);
                elTable.Add(elColumn);
            }

            return elTable;
        }

        private static XElement createXmlColumn(IColumnInfo col)
        {
            col.Attributes["local_desc"] = col.Description;
            col.Attributes["new_desc"] = "";

            XElement elColumn = new XElement("column");
            elColumn.Add(new XAttribute("rawname", col.RawName));
            elColumn.Add(new XAttribute("dbtype", col.DBType));
            elColumn.Add(new XAttribute("dbtargettype", col.DbTargetType));
            elColumn.Add(new XAttribute("csharptype", col.CSharpType));
            elColumn.Add(new XAttribute("desc", col.Description));
            elColumn.Add(new XAttribute("nullable", col.Nullable));
            elColumn.Add(new XAttribute("iskey", col.IsPrimaryKey));
            elColumn.Add(new XAttribute("maxlength", col.MaxLength));
            elColumn.Add(new XAttribute("precision", col.Precision));
            elColumn.Add(new XAttribute("scale", col.Scale));
            elColumn.Add(new XAttribute("identity", col.Identity));
            elColumn.Add(new XAttribute("computed", col.Computed));

            return elColumn;
        }

        private static void updateXmlColumn(XElement el, IColumnInfo col)
        {
            el.Attribute("rawname").SetValue(col.RawName);
            el.Attribute("dbtype").SetValue(col.DBType);
            el.Attribute("dbtargettype").SetValue(col.DbTargetType);
            el.Attribute("csharptype").SetValue(col.CSharpType);
            //el.Attribute("desc").SetValue(col.Description);
            el.Attribute("nullable").SetValue(col.Nullable);
            el.Attribute("iskey").SetValue(col.IsPrimaryKey);
            el.Attribute("maxlength").SetValue(col.MaxLength);
            el.Attribute("precision").SetValue(col.Precision);
            el.Attribute("scale").SetValue(col.Scale);
            el.Attribute("identity").SetValue(col.Identity);
            el.Attribute("computed").SetValue(col.Computed);
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
                    elColumn.Add(new XAttribute("csharptype", col.CSharpType));
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
