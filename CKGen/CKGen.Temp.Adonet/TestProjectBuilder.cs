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
        public string Build(List<ITableInfo> tables, List<IViewInfo> views, string connStr, string projName)
        {
            Guid testProjID = Guid.NewGuid();

            string root = Path.Combine(Path.GetTempPath(), testProjID.ToString("P"));
            Directory.CreateDirectory(root);

            TestProjectModel model = new TestProjectModel();
            model.ProjectGuid = testProjID.ToString().ToUpper();
            model.ProjectGuidLower = testProjID.ToString().ToLower();
            model.ProjectName = projName;
            model.ConnectionString = connStr;
            foreach (var tinfo in tables)
            {
                model.Files.Add(string.Format(@"Model\{0}.cs", tinfo.PascalName));
                model.Files.Add(string.Format(@"DAL\{0}Access.cs", tinfo.PascalName));
            }

            //sln
            string slnFilePath = Path.Combine(root, string.Format("{0}.sln", model.ProjectName));
            string slnContent = codeGen.Gen(Comm.GetProjTemplete("sln.cshtml"), model);
            File.WriteAllText(slnFilePath, slnContent);

            //testapp proj folder
            string testappFolder = Path.Combine(root, model.ProjectName);
            Directory.CreateDirectory(testappFolder);

            //testapp csproj
            string csprojFilePath = Path.Combine(testappFolder, string.Format("{0}.csproj", model.ProjectName));
            string csprojContent = codeGen.Gen(Comm.GetProjTemplete("csproj.cshtml"), model);
            File.WriteAllText(csprojFilePath, csprojContent);

            //Properties
            string PropertiesFolder = Path.Combine(testappFolder, "Properties");
            Directory.CreateDirectory(PropertiesFolder);

            //AssemblyInfo.cs
            string assemblyFilePath = Path.Combine(PropertiesFolder, "AssemblyInfo.cs");
            string assemblyContent = codeGen.Gen(Comm.GetProjTemplete("AssemblyInfo.cshtml"), model);
            File.WriteAllText(assemblyFilePath, assemblyContent);

            //program.cs
            string programFilePath = Path.Combine(testappFolder, "Program.cs");
            string programContent = codeGen.Gen(Comm.GetProjTemplete("Program.cshtml"), model);
            File.WriteAllText(programFilePath, programContent);

            //app.config
            string configFilePath = Path.Combine(testappFolder, "App.config");
            string configContent = codeGen.Gen(Comm.GetProjTemplete("appconfig.cshtml"), model);
            File.WriteAllText(configFilePath, configContent);

            //packages.config
            string pkgconfigFilePath = Path.Combine(testappFolder, "packages.config");
            string pkgconfigContent = codeGen.Gen(Comm.GetProjTemplete("packagesconfig.cshtml"), model);
            File.WriteAllText(pkgconfigFilePath, pkgconfigContent);

            //loghelper.cs
            string loghelperFilePath = Path.Combine(testappFolder, "LogHelper.cs");
            string loghelperContent = codeGen.Gen(Comm.GetProjTemplete("loghelper.cshtml"), model);
            File.WriteAllText(loghelperFilePath, loghelperContent);

            foreach (var tinfo in tables)
            {
                DbTableCodeGen accessgen = new DbTableCodeGen();
                //model.cs
                string modelFilePath = Path.Combine(testappFolder, string.Format(@"Model\{0}.cs", tinfo.PascalName));
                string modelContent = accessgen.GenModelCode(tinfo);//codeGen.Gen(Comm.GetTemplete("Model.cshtml"), tinfo);
                var dir = Directory.GetParent(modelFilePath);
                if(!dir.Exists)
                {
                    dir.Create();
                }
                File.WriteAllText(modelFilePath, modelContent.Replace("'namespace'", projName));

                //access.cs
                string accessFilePath = Path.Combine(testappFolder, string.Format(@"DAL\{0}Access.cs", tinfo.PascalName));
                string accessContent = accessgen.GenDataAccessCode(projName, tinfo);
                dir = Directory.GetParent(accessFilePath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                File.WriteAllText(accessFilePath, accessContent.Replace("'conn_name'", "Program.TestConnection"));
            }

            foreach (var vinfo in views)
            {
                DbViewCodeGen accessgen = new DbViewCodeGen();
                //model.cs
                string modelFilePath = Path.Combine(testappFolder, string.Format(@"Model\{0}.cs", vinfo.PascalName));
                string modelContent = accessgen.GenModelCode(vinfo);//codeGen.Gen(Comm.GetTemplete("Model.cshtml"), vinfo);
                var dir = Directory.GetParent(modelFilePath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                File.WriteAllText(modelFilePath, modelContent.Replace("'namespace'", projName));

                //access.cs
                string accessFilePath = Path.Combine(testappFolder, string.Format(@"DAL\{0}Access.cs", vinfo.PascalName));
                string accessContent = accessgen.GenDataAccessCode(projName, vinfo);
                dir = Directory.GetParent(accessFilePath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                File.WriteAllText(accessFilePath, accessContent.Replace("'conn_name'", "Program.TestConnection"));
            }

            return root;
        }

        //public string Gen(string viewname, object model)
        //{
        //    //string viewPath = System.IO.Path.Combine(Environment.CurrentDirectory, viewname);
        //    //return codeGen.GenByPath(viewPath, model);
        //    string tmp = Comm.GetProjTemplete(viewname);
        //    return codeGen.Gen(tmp, model);
        //}
    }
}
