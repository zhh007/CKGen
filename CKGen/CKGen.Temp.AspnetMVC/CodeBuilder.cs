using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using CKGen.DBSchema;

namespace CKGen.Temp.AspnetMVC
{
    public class CodeBuilder
    {
        private string _namespace;
        private string _webProjNameSpace;
        private string _dbConnection;
        private string _targetFolder;
        private string _databaseName;
        private string _tableName;

        public CodeBuilder(string dbConnStr, string dbName, string targetFoler, string tableName, string ns, string webns)
        {
            if (string.IsNullOrEmpty(dbName))
                throw new Exception("数据库名不能为空。");

            if (string.IsNullOrEmpty(dbConnStr))
                throw new Exception("数据库链接不能为空。");

            if (string.IsNullOrEmpty(targetFoler))
                throw new Exception("目标文件夹不能为空。");

            this._namespace = ns;
            this._webProjNameSpace = webns;
            this._dbConnection = dbConnStr;
            this._targetFolder = Path.Combine(targetFoler, "");//AutoEntites
            this._databaseName = dbName;
            this._tableName = tableName;

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

        public string Build(IDatabaseInfo database)
        {
            //IServerInfo serverInfo = new ServerInfo(this._dbConnection, "ceshi");

            List<string> filePaths = new List<string>();

            //IDatabaseInfo database = serverInfo.GetDatabase(_databaseName);

            foreach (ITableInfo tInfo in database.Tables)
            {
                if (tInfo.LowerName != _tableName.ToLower())
                {
                    continue;
                }

                PageViewModel pvModel = new PageViewModel();
                pvModel.NameSpacePR = this._namespace;
                pvModel.WebProjNameSpace = this._webProjNameSpace;
                pvModel.DBTable = tInfo;

                _build("Model.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + ".cs");
                _build("Map.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Map.cs");
                _build("Repository.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Repository.cs");
                _build("DTO.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "DTO.cs");
                _build("IService.cshtml", typeof(PageViewModel), pvModel, "I" + tInfo.PascalName + "Service.cs");
                _build("Service.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Service.cs");

                _build("Controller.cshtml", typeof(PageViewModel), pvModel, "Controller.cs");
                _build("ViewModel.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "ViewModel.cs");
                _build("Index.cshtml", typeof(PageViewModel), pvModel, "Index.cshtml");
                _build("Create.cshtml", typeof(PageViewModel), pvModel, "Create.cshtml");
                _build("Edit.cshtml", typeof(PageViewModel), pvModel, "Edit.cshtml");
            }
            return this._targetFolder;
        }

        private void _build(string viewname, Type viewModelType, object viewmodel, string targetFileName)
        {
            string tmp = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "AspnetMVC\\" + viewname));
            string result = GetRazor().RunCompile(tmp, viewname, viewModelType, viewmodel);

            string filePath = Path.Combine(_targetFolder, targetFileName);
            if (!Directory.Exists(this._targetFolder))
            {
                Directory.CreateDirectory(this._targetFolder);
            }
            using (var sw = new StreamWriter(File.Open(filePath, FileMode.CreateNew), System.Text.Encoding.UTF8))
            {
                sw.Write(result);
            }
        }

        private static IRazorEngineService GetRazor()
        {
            TemplateServiceConfiguration config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true; // loads the files in-memory (gives the templates full-trust permissions)
            config.CachingProvider = new DefaultCachingProvider(t => { }); //disables the warnings
            config.EncodedStringFactory = new RazorEngine.Text.RawStringFactory(); // Raw string encoding.

            // Use the config
            Engine.Razor = RazorEngineService.Create(config); // new API

            return Engine.Razor;
        }
    }
}
