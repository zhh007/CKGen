// -----------------------------------------------------------------------
// <copyright file="TableCodeBuiler.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CKGen.DBSchema;
    using System.IO;
    using CodeTempletes;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TableCodeBuiler
    {
        private string _namespace;
        private ITableInfo _tableInfo;
        private string _databaseName;

        public TableCodeBuiler(string namespaceString, ITableInfo tableInfo, string dbname)
        {
            this._namespace = namespaceString;
            this._tableInfo = tableInfo;
            this._databaseName = dbname;
        }

        //public TableCodeBuiler(string namespaceString, TableInfo tableInfo, string targetFolder)
        //    : this(namespaceString, tableInfo)
        //{
        //    this._folderPath = targetFolder;
        //}

        public string ModelBuild()
        {
            TableModelTemplete tableCode = new TableModelTemplete(this._namespace, this._tableInfo);
            string codeString = tableCode.TransformText();
            return codeString;
        }

        public string ModelBuildTo(string targetFolder)
        {
            string codeString = ModelBuild();

            string filePath = Path.Combine(targetFolder, this._tableInfo.Name + "Model.cs");
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(codeString);
                sw.Close();
            }

            return filePath;
        }

        public string DBLinkBuild()
        {
            DBLinkTemplete tableCode = new DBLinkTemplete(this._namespace, this._databaseName);
            string codeString = tableCode.TransformText();
            return codeString;
        }

        public string DBLinkBuildTo(string targetFolder)
        {
            string codeString = DBLinkBuild();

            string filePath = Path.Combine(targetFolder, this._databaseName + "Link.cs");
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(codeString);
                sw.Close();
            }

            return filePath;
        }

        public string SchemaBuild()
        {
            TableSchemaTemplete tableCode = new TableSchemaTemplete(this._namespace, this._tableInfo);
            string codeString = tableCode.TransformText();
            return codeString;
        }

        public string SchemaBuildTo(string targetFolder)
        {
            string codeString = SchemaBuild();

            string filePath = Path.Combine(targetFolder, this._tableInfo.Name + "Schema.cs");
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(codeString);
                sw.Close();
            }

            return filePath;
        }

        public string DtoBuild()
        {
            TableDTOTemplete tableCode = new TableDTOTemplete(this._namespace, this._tableInfo);
            string codeString = tableCode.TransformText();
            return codeString;
        }

        public string DtoBuildTo(string targetFolder)
        {
            string codeString = DtoBuild();

            string filePath = Path.Combine(targetFolder, this._tableInfo.Name + "Dao.cs");
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(codeString);
                sw.Close();
            }

            return filePath;
        }

        public string EntityBuild()
        {
            AutoEntitiesTemplete tableCode = new AutoEntitiesTemplete(this._namespace, this._tableInfo);
            string codeString = tableCode.TransformText();
            return codeString;
        }

        public string EntityBuildTo(string targetFolder)
        {
            string codeString = EntityBuild();

            string filePath = Path.Combine(targetFolder, this._tableInfo.Name + "Entity.cs");
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(codeString);
                sw.Close();
            }

            return filePath;
        }
    }
}
