using CKGen.DBLoader;
using CKGen.DBSchema;
using CKGen.Temp.DBDoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("正在生成...");

            BuildDBDoc();

            Console.WriteLine("over!");
            Console.ReadKey();
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
    }
}
