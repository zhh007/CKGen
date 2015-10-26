using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

/*
<database name="">
    <table rawname="" schema="">
        <column rawname="" dbtype="" sqldatatype="" csharptype="" desc="" nullable="" iskey="" maxlength="" precision="" scale="" identity="" computed="" />
    </table>
</database>
*/
namespace CKGen
{
    public class DatabaseSchemaSetting
    {
        public static IDatabaseInfo Compute(IDatabaseInfo db)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Settings\\" + "db_" + SystemConfig.DBName + ".xml");
            if (File.Exists(filePath))
            {
                string txt = File.ReadAllText(filePath);
                XElement root = XElement.Parse(txt);

                foreach (ITableInfo tb in db.Tables)
                {
                    var el = (from p in root.Elements()
                              where p.Attribute("rawname").Value == tb.RawName
                              select p).FirstOrDefault();
                    if (el != null)
                    {
                        foreach (IColumnInfo col in tb.Columns)
                        {
                            string local_desc = "";
                            var colEl = (from c in el.Elements()
                                         where c.Attribute("rawname").Value == col.RawName
                                         select c).FirstOrDefault();
                            if (colEl != null)
                            {
                                local_desc = colEl.Attribute("desc").Value;
                            }
                            col.Attributes["local_desc"] = local_desc;
                            if (string.IsNullOrEmpty(col.Description) && !string.IsNullOrEmpty(local_desc))
                            {
                                col.Attributes["new_desc"] = local_desc;
                            }
                            else
                            {
                                col.Attributes["new_desc"] = "";
                            }
                        }
                    }
                }
            }
            else
            {
                //save
                XElement root = new XElement("database");

                foreach (ITableInfo tb in db.Tables)
                {
                    XElement elTable = new XElement("table");
                    elTable.Add(new XAttribute("rawname", tb.RawName));
                    elTable.Add(new XAttribute("schema", tb.Schema));

                    foreach (IColumnInfo col in tb.Columns)
                    {
                        col.Attributes["local_desc"] = col.Description;
                        col.Attributes["new_desc"] = "";

                        XElement elColumn = new XElement("column");
                        elColumn.Add(new XAttribute("rawname", col.RawName));
                        elColumn.Add(new XAttribute("dbtype", col.DBType));
                        elColumn.Add(new XAttribute("sqldatatype", col.SqlDataType));
                        elColumn.Add(new XAttribute("csharptype", col.CSharpType));
                        elColumn.Add(new XAttribute("desc", col.Description));
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
                //string filePath = Path.Combine(Environment.CurrentDirectory, FileName);
                xdoc.Save(filePath);
            }

            return db;
        }

        public static void Save(IDatabaseInfo db)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "Settings\\" + "db_" + SystemConfig.DBName + ".xml");

            //save
            XElement root = new XElement("database");

            foreach (ITableInfo tb in db.Tables)
            {
                XElement elTable = new XElement("table");
                elTable.Add(new XAttribute("rawname", tb.RawName));
                elTable.Add(new XAttribute("schema", tb.Schema));

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
                    elColumn.Add(new XAttribute("sqldatatype", col.SqlDataType));
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
            //string filePath = Path.Combine(Environment.CurrentDirectory, FileName);
            xdoc.Save(filePath);

        }


    }
}
