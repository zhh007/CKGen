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
        private string _targetFolder;

        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();

        public CodeBuilder()
        {
            Guid testProjID = Guid.NewGuid();
            _targetFolder = Path.Combine(Path.GetTempPath(), testProjID.ToString("P"));
            Directory.CreateDirectory(_targetFolder);
        }

        public string Build(IDatabaseInfo database, string tableName, string ns, string webns)
        {
            try
            {
                if (!Directory.Exists(_targetFolder))
                {
                    Directory.CreateDirectory(_targetFolder);
                }
                else
                {
                    Directory.Delete(_targetFolder, true);
                    Directory.CreateDirectory(_targetFolder);
                }
            }
            catch (Exception)
            {
                throw new Exception("目标文件夹创建失败。");
            }

            List<string> filePaths = new List<string>();
            foreach (ITableInfo tInfo in database.Tables)
            {
                if (tInfo.LowerName != tableName.ToLower())
                {
                    continue;
                }

                Build(tInfo, ns, webns);
            }
            return _targetFolder;
        }

        public string Build(ITableInfo tInfo, string ns, string webns)
        {
            PageViewModel pvModel = new PageViewModel();
            pvModel.NameSpacePR = ns;
            pvModel.WebProjNameSpace = webns;
            pvModel.DBTable = tInfo;

            _build("Model.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + ".cs");
            _build("Map.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Map.cs");
            _build("Repository.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Repository.cs");
            _build("DTO.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "DTO.cs");
            _build("IService.cshtml", typeof(PageViewModel), pvModel, "I" + tInfo.PascalName + "Service.cs");
            _build("Service.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Service.cs");

            _build("Controller.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Controller.cs");
            _build("ViewModel.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "ViewModel.cs");
            _build("Index.cshtml", typeof(PageViewModel), pvModel, "Index.cshtml");
            _build("Add.cshtml", typeof(PageViewModel), pvModel, "Add.cshtml");
            _build("Edit.cshtml", typeof(PageViewModel), pvModel, "Edit.cshtml");

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
