using CKGen.Base;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CKGen.Temp.Adonet
{
    public class TestProjectBuilder
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();
        public string Build(ITableInfo info, string connStr)
        {
            Guid testProjID = Guid.NewGuid();

            string root = Path.Combine(Path.GetTempPath(), testProjID.ToString("P"));
            Directory.CreateDirectory(root);

            TestProjectModel model = new TestProjectModel();
            model.ProjectGuid = testProjID.ToString().ToUpper();
            model.ProjectGuidLower = testProjID.ToString().ToLower();
            model.ProjectName = "TestApp";
            model.ModelClassName = info.PascalName;
            model.ConnectionString = connStr;

            //sln
            string slnFilePath = Path.Combine(root, string.Format("{0}.sln", model.ProjectName));
            string slnContent = Gen("Proj\\sln.cshtml", model);
            File.WriteAllText(slnFilePath, slnContent);

            //testapp proj folder
            string testappFolder = Path.Combine(root, model.ProjectName);
            Directory.CreateDirectory(testappFolder);

            //testapp csproj
            string csprojFilePath = Path.Combine(testappFolder, string.Format("{0}.csproj", model.ProjectName));
            string csprojContent = Gen("Proj\\csproj.cshtml", model);
            File.WriteAllText(csprojFilePath, csprojContent);

            //Properties
            string PropertiesFolder = Path.Combine(testappFolder, "Properties");
            Directory.CreateDirectory(PropertiesFolder);

            //AssemblyInfo.cs
            string assemblyFilePath = Path.Combine(PropertiesFolder, "AssemblyInfo.cs");
            string assemblyContent = Gen("Proj\\AssemblyInfo.cshtml", model);
            File.WriteAllText(assemblyFilePath, assemblyContent);

            //program.cs
            string programFilePath = Path.Combine(testappFolder, "Program.cs");
            string programContent = Gen("Proj\\Program.cshtml", model);
            File.WriteAllText(programFilePath, programContent);

            //app.config
            string configFilePath = Path.Combine(testappFolder, "App.config");
            string configContent = Gen("Proj\\appconfig.cshtml", model);
            File.WriteAllText(configFilePath, configContent);

            //model.cs
            string modelFilePath = Path.Combine(testappFolder, model.ModelClassName + ".cs");
            string modelContent = Gen("Template\\Model.cshtml", info);
            File.WriteAllText(modelFilePath, modelContent);

            //access.cs
            TableDataAccessGen accessgen = new TableDataAccessGen();
            string accessFilePath = Path.Combine(testappFolder, model.ModelClassName + "Access.cs");
            string accessContent = accessgen.GenDataAccessCode("TestApp", info);
            File.WriteAllText(accessFilePath, accessContent.Replace("'conn_name'", "Program.TestConnection"));

            return root;
        }

        public string Gen(string viewname, object model)
        {
            string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, viewname);
            return codeGen.Gen(viewPath, model);
        }
    }
}
