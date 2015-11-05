using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CKGen.DBSchema;
using CKGen.Base;

namespace CKGen.Temp.AspnetMVC
{
    public class CodeBuilder
    {
        private IDatabaseInfo _database;
        private string _namespace;
        private string _webProjNameSpace;
        private string _targetFolder;
        private string _tableName;

        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();

        public CodeBuilder(IDatabaseInfo database, string tableName, string ns, string webns)
        {
            this._database = database;
            this._targetFolder = Path.Combine(Environment.CurrentDirectory, database.Name);
            this._tableName = tableName;
            this._namespace = ns;
            this._webProjNameSpace = webns;

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

        public string Build()
        {
            List<string> filePaths = new List<string>();
            foreach (ITableInfo tInfo in _database.Tables)
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
            string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "AspnetMVC\\" + viewname);
            string filePath = Path.Combine(_targetFolder, targetFileName);
            codeGen.GenSave(viewPath, viewmodel, filePath);
        }
    }
}
