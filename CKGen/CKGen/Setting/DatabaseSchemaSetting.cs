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
                forUpdate2(filePath, db);
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

        /// <summary>
        /// 执行时间:24.8361358秒
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="db"></param>
        private static void forUpdate1(string filePath, IDatabaseInfo db)
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

        private static void forUpdate2(string filePath, IDatabaseInfo db)
        {
            string txt = File.ReadAllText(filePath);
            XElement root = XElement.Parse(txt);

            int size = db.Tables.Count;
            int N = size;
            int P = Environment.ProcessorCount; // assume twice the procs for 
                                                // good work distribution
            int Chunk = (int)Math.Round((double)N / (double)P, 0);// size of a work chunk
            AutoResetEvent signal = new AutoResetEvent(false);
            int counter = P;                        // use a counter to reduce 
                                                    // kernel transitions    

            Debug.WriteLine("size:{0}", size);
            Debug.WriteLine("P:{0}", P);
            Debug.WriteLine("Chunk:{0}", Chunk);

            for (int c = 0; c < P; c++)
            {           // for each chunk
                //Thread th = new Thread(new ParameterizedThreadStart((o) =>
                //{
                //    int lc = (int)o;
                //    int start = lc * Chunk;// iterate through a work chunk
                //    int end = (lc + 1 == P ? N : (lc + 1) * Chunk);// respect upper bound
                //    Debug.WriteLine("{0} : {1} ~ {2}", lc, start, end);

                //    for (int i = start; i < end; i++)
                //    {
                //        ITableInfo tb = db.Tables[i];
                //        NewMethod(root, tb);
                //    }
                //    if (Interlocked.Decrement(ref counter) == 0)
                //    { // use efficient 
                //      // interlocked 
                //      // instructions      
                //        signal.Set();  // and kernel transition only when done
                //    }
                //}));
                //th.IsBackground = true;
                //th.Start(c);
                ThreadPool.QueueUserWorkItem(delegate (Object o)
                {
                    int lc = (int)o;
                    int start = lc * Chunk;// iterate through a work chunk
                    int end = (lc + 1 == P ? N : (lc + 1) * Chunk);// respect upper bound
                    Debug.WriteLine("{0} : {1} ~ {2}", lc, start, end);

                    for (int i = start; i < end; i++)
                    {
                        ITableInfo tb = db.Tables[i];
                        NewMethod(root, tb);
                    }
                    if (Interlocked.Decrement(ref counter) == 0)
                    { // use efficient 
                      // interlocked 
                      // instructions      
                        signal.Set();  // and kernel transition only when done
                    }
                }, c);
            }
            signal.WaitOne();
        }

        private static void NewMethod(XElement root, ITableInfo tb)
        {
            foreach (IColumnInfo col in tb.Columns)
            {
            }

            //var el = (from p in root.Elements()
            //          where p.Attribute("rawname").Value == tb.RawName
            //          select p).FirstOrDefault();
            //if (el != null)
            //{
            //    foreach (IColumnInfo col in tb.Columns)
            //    {
            //        string local_desc = "";
            //        var colEl = (from c in el.Elements()
            //                     where c.Attribute("rawname").Value == col.RawName
            //                     select c).FirstOrDefault();
            //        if (colEl != null)
            //        {
            //            local_desc = colEl.Attribute("desc").Value;
            //        }
            //        col.Attributes["local_desc"] = local_desc;
            //        if (string.IsNullOrEmpty(col.Description) && !string.IsNullOrEmpty(local_desc))
            //        {
            //            col.Attributes["new_desc"] = local_desc;
            //        }
            //        else
            //        {
            //            col.Attributes["new_desc"] = "";
            //        }
            //    }
            //}
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
