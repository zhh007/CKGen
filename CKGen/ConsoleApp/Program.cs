using CKGen;
using CKGen.Base;
using CKGen.DBLoader;
using CKGen.DBSchema;
using CKGen.Services;
using CKGen.Temp.Adonet;
using CKGen.Temp.DataAccess;
using CKGen.Temp.DBDoc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceLocator.Instance.AddService<ICodeGenService>(new CodeGenService());
            Console.WriteLine("正在生成...");

            BuildProj();

            //BuildDBAccess();

            //TestProjectBuilder builder = new TestProjectBuilder();
            //string folder = builder.Build();

            //Process.Start(folder);

            //BuildTableAccess();

            Console.WriteLine("over!");
            //Console.ReadKey();
        }

        private static void BuildDBDoc()
        {
            string dbConnStr = @"Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Password=pass@word1;Initial Catalog=ENTERPRISES_SUPERWORKFLOW;Data Source=.\SQL2008R2";
            string dbname = "ENTERPRISES_SUPERWORKFLOW";
            ServerInfo serverInfo = new ServerInfo(dbConnStr, dbname);
            IDatabaseInfo database = serverInfo.GetDatabase(dbname);

            DBDocBuilder builder = new DBDocBuilder(database, dbname);
            builder.Build();
        }

        private static void BuildDBAccess()
        {
            //Console.WriteLine("正在生成...");

            var watch1 = Stopwatch.StartNew();

            string dbConnStr = @"Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Password=pass@word1;Initial Catalog=ENTERPRISES_SUPERWORKFLOW;Data Source=.\SQL2008R2";

            string solutionName = "CK";
            string dbname = "ENTERPRISES_SUPERWORKFLOW";
            ServerInfo serverInfo = new ServerInfo(dbConnStr, dbname);
            IDatabaseInfo database = serverInfo.GetDatabase(dbname);

            SolutionBuilder sb = new SolutionBuilder(database, solutionName, dbname, dbConnStr);
            string errorStr = "";
            if (!sb.Build(ref errorStr))
            {
                Console.Write(errorStr);
            }

            Console.WriteLine("over!");

            Console.WriteLine("{0} ", watch1.Elapsed);
            Console.ReadKey();
        }

        private static void BuildTableAccess()
        {
            string dbConnStr = @"Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Password=pass@word1;Initial Catalog=DBTest;Data Source=.\SQL2008R2";
            string dbname = "DBTest";
            string tableName = "df_TestUser";
            ServerInfo serverInfo = new ServerInfo(dbConnStr, dbname);
            IDatabaseInfo database = serverInfo.GetDatabase(dbname);

            ITableInfo tbInfo = null;

            foreach (ITableInfo tInfo in database.Tables)
            {
                if (tInfo.LowerName == tableName.ToLower())
                {
                    tbInfo = tInfo;
                    break;
                }
            }

            TableDataAccessGen gen = new TableDataAccessGen();

            //string code = CodeGen.GenForTable("Model.cshtml", tbInfo);

            string code = gen.Build("TestApp", tbInfo);

            string fileName = System.IO.Path.GetTempFileName();

            System.IO.File.WriteAllText(fileName, code);

            Process.Start("notepad.exe", fileName);
        }

        private static void BuildProj()
        {
            string dbConnStr = @"Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Password=pass@word1;Initial Catalog=DBTest;Data Source=.\SQL2008R2";
            string dbname = "DBTest";
            string tableName = "SampleInt";
            string connStr2 = @"Data Source=.\SQL2008R2;Initial Catalog=DBTest;User ID=sa;Password=pass@word1;Persist Security Info=False;";
            ServerInfo serverInfo = new ServerInfo(dbConnStr, dbname);
            IDatabaseInfo database = serverInfo.GetDatabase(dbname);

            ITableInfo tbInfo = null;

            foreach (ITableInfo tInfo in database.Tables)
            {
                if (tInfo.LowerName == tableName.ToLower())
                {
                    tbInfo = tInfo;
                    break;
                }
            }

            TestProjectBuilder builder = new TestProjectBuilder();
            string folder = builder.Build(tbInfo, connStr2);
            Process.Start(folder);
        }
    }
}
