using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CKGen.DBSchema;
using CKGen.Base;

namespace CKGen.Temp.AspnetMVC2
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

        public string Build(IDatabaseInfo database, string tableName, string ns, string modalName, string dbContext)
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

                Build(tInfo, ns, modalName, dbContext);
            }
            return _targetFolder;
        }

        public string Build(ITableInfo tInfo, string ns, string entityName, string dbContext)
        {
            PageViewModel pvModel = new PageViewModel();
            pvModel.NameSpacePR = ns;
            pvModel.DBTable = tInfo;
            pvModel.DatabaseName = tInfo.Database.Name;
            pvModel.TableName = entityName;
            pvModel.DBContextTypeName = dbContext;

            _build("Model.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + ".cs");
            _build("Map.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Map.cs");
            _build("Repository.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Repository.cs");
            _build("DTO.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "DTO.cs");
            _build("IService.cshtml", typeof(PageViewModel), pvModel, "I" + tInfo.PascalName + "Service.cs");
            _build("Service.cshtml", typeof(PageViewModel), pvModel, tInfo.PascalName + "Service.cs");

            _build("readme.cshtml", typeof(PageViewModel), pvModel, "readme.txt");

            return this._targetFolder;
        }

        private void _build(string viewname, Type viewModelType, object viewmodel, string targetFileName)
        {
            string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, "AspnetMVC2\\" + viewname);
            string filePath = Path.Combine(_targetFolder, targetFileName);
            codeGen.GenSave(viewPath, viewmodel, filePath);
        }

        //public string BuildSimpleChildEdit(SimpleChildGenModel model)
        //{
        //    _build("SimpleChildUI\\_ChildEdit.cshtml", typeof(SimpleChildGenModel), model, "_" + model.ChildModel.PascalName + "Edit.cshtml");
        //    _build("SimpleChildUI\\ParentService.cshtml", typeof(SimpleChildGenModel), model, model.ParentModel.PascalName + "Service.cs");
        //    _build("SimpleChildUI\\ParentViewModel.cshtml", typeof(SimpleChildGenModel), model, model.ParentModel.PascalName + "ViewModel.cs");
        //    _build("SimpleChildUI\\ParentDTO.cshtml", typeof(SimpleChildGenModel), model, model.ParentModel.PascalName + "DTO.cs");
        //    _build("SimpleChildUI\\ParentModel.cshtml", typeof(SimpleChildGenModel), model, model.ParentModel.PascalName + ".cs");

        //    _build("SimpleChildUI\\ChildModel.cshtml", typeof(SimpleChildGenModel), model, model.ChildModel.PascalName + ".cs");
        //    _build("SimpleChildUI\\ChildMap.cshtml", typeof(PageViewModel), model, model.ChildModel.PascalName + "Map.cs");

        //    _build("SimpleChildUI\\readme.cshtml", typeof(SimpleChildGenModel), model, "readme.txt");

        //    return this._targetFolder;
        //}
    }
}
