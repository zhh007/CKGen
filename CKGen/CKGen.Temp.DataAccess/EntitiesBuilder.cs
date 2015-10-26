using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using CKGen.DBSchema;
using System.Data.OleDb;

namespace CKGen.Temp.DataAccess
{
    public class EntitiesBuilder
    {
        private IDatabaseInfo _database;
        private string _targetFolder;
        private string _namespace;
        private string _dbname;

        public EntitiesBuilder(IDatabaseInfo database, string dbname, string nameSpace, string targetFoler)
        {
            if (database == null)
                throw new Exception("数据库不能为空。");
            if (string.IsNullOrEmpty(targetFoler))
                throw new Exception("目标文件夹不能为空。");
            if (string.IsNullOrEmpty(nameSpace))
                throw new Exception("命名空间不能为空。");

            this._database = database;
            this._targetFolder = Path.Combine(targetFoler, "");
            this._dbname = dbname;
            this._namespace = nameSpace;

            //OleDbConnection conn = new OleDbConnection(dbConnStr);
            //try
            //{
            //    conn.Open();
            //}
            //catch (Exception)
            //{
            //    throw new Exception("数据库连接失败。");
            //}
            //finally
            //{
            //    conn.Close();
            //}

            try
            {
                if (!Directory.Exists(this._targetFolder))
                {
                    Directory.CreateDirectory(this._targetFolder);
                }
                else
                {
                    Directory.Delete(this._targetFolder, true);
                    Directory.CreateDirectory(this._targetFolder);
                }
            }
            catch (Exception)
            {
                throw new Exception("目标文件夹创建失败。");
            }
        }

        public List<string> Build()
        {
            List<string> filePaths = new List<string>();
            Var.DBConnectionName = this._dbname;

            int i = 0;
            foreach (var tInfo in _database.Tables)
            {
                TableCodeBuiler codeBuilder = new TableCodeBuiler(this._namespace, tInfo, this._dbname);

                //if (table.Name != "BX_CANCELBORROW")
                //    continue;
                //string codeStr;
                //string filePath;

                if (i == 0)
                {                    
                    string linkFilePath = codeBuilder.DBLinkBuildTo(this._targetFolder);
                    filePaths.Add(linkFilePath);
                }

                //TableSchema                
                string schemaFolder = Path.Combine(this._targetFolder, "Schema");
                string schemaFilePath = codeBuilder.SchemaBuildTo(schemaFolder);                
                filePaths.Add(schemaFilePath);

                //TableModle
                string tableFolder = Path.Combine(this._targetFolder, "Model");
                string modelFilePath = codeBuilder.ModelBuildTo(tableFolder);
                filePaths.Add(modelFilePath);

                //dto                
                string dtoFolder = Path.Combine(this._targetFolder, "DAO");
                string dtoFilePath = codeBuilder.DtoBuildTo(dtoFolder);                
                filePaths.Add(dtoFilePath);

                //entity
                string autoFolder = Path.Combine(this._targetFolder, "Entities");
                string autoFilePath = codeBuilder.EntityBuildTo(autoFolder);
                filePaths.Add(autoFilePath);

                i++;
            }

            return filePaths;
        }
    }
}
