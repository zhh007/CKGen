using CKGen.Base;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CKGen.Temp.Adonet.DbConsoleProject
{
    public class DbProjectBuilder
    {
        private readonly ICodeGenService codeGen = ServiceLocator.Instance.GetService<ICodeGenService>();
        public string TempPath_Sln = GetTemplete("sln.cshtml");
        public string TempPath_ConsoleProj_AssemblyInfo = GetTemplete("ConsoleProj.AssemblyInfo.cshtml");
        public string TempPath_ConsoleProj_Program = GetTemplete("ConsoleProj.Program.cshtml");
        public string TempPath_ConsoleProj_AppConfig = GetTemplete("ConsoleProj.appconfig.cshtml");
        public string TempPath_ConsoleProj_PackageConfig = GetTemplete("ConsoleProj.packagesconfig.cshtml");
        public string TempPath_ConsoleProj_LogHelper = GetTemplete("ConsoleProj.loghelper.cshtml");
        public string TempPath_ConsoleProj_CSProj = GetTemplete("ConsoleProj.csproj.cshtml");

        public static string GetTemplete(string tmp)
        {
            string tmpName = string.Format("CKGen.Temp.Adonet.DbConsoleProject.Sln.{0}", tmp);
            Assembly myAssembly = typeof(Comm).Assembly;
            using (Stream input = myAssembly.GetManifestResourceStream(tmpName))
            using (StreamReader reader = new StreamReader(input, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public string Build(List<ITableInfo> tables, List<IViewInfo> views, string connStr, string projName)
        {
            Guid testProjID = Guid.NewGuid();

            string root = Path.Combine(Path.GetTempPath(), testProjID.ToString("P"));
            Directory.CreateDirectory(root);

            DbProjectModel model = new DbProjectModel();
            model.ProjectGuid = testProjID.ToString().ToUpper();
            model.ProjectGuidLower = testProjID.ToString().ToLower();
            model.ProjectName = projName;
            model.ConnectionString = connStr;

            //sln
            string slnFilePath = Path.Combine(root, string.Format("{0}.sln", model.ProjectName));
            string slnContent = codeGen.Gen(this.TempPath_Sln, model);
            File.WriteAllText(slnFilePath, slnContent);

            //testapp proj folder
            string testappFolder = Path.Combine(root, model.ProjectName);
            Directory.CreateDirectory(testappFolder);

            //Properties
            string PropertiesFolder = Path.Combine(testappFolder, "Properties");
            Directory.CreateDirectory(PropertiesFolder);

            //AssemblyInfo.cs
            string assemblyFilePath = Path.Combine(PropertiesFolder, "AssemblyInfo.cs");
            string assemblyContent = codeGen.Gen(this.TempPath_ConsoleProj_AssemblyInfo, model);
            File.WriteAllText(assemblyFilePath, assemblyContent);

            //program.cs
            string programFilePath = Path.Combine(testappFolder, "Program.cs");
            string programContent = codeGen.Gen(this.TempPath_ConsoleProj_Program, model);
            File.WriteAllText(programFilePath, programContent);

            //app.config
            string configFilePath = Path.Combine(testappFolder, "App.config");
            string configContent = codeGen.Gen(this.TempPath_ConsoleProj_AppConfig, model);
            File.WriteAllText(configFilePath, configContent);

            //packages.config
            string pkgconfigFilePath = Path.Combine(testappFolder, "packages.config");
            string pkgconfigContent = codeGen.Gen(this.TempPath_ConsoleProj_PackageConfig, model);
            File.WriteAllText(pkgconfigFilePath, pkgconfigContent);

            //loghelper.cs
            string loghelperFilePath = Path.Combine(testappFolder, "LogHelper.cs");
            string loghelperContent = codeGen.Gen(this.TempPath_ConsoleProj_LogHelper, model);
            File.WriteAllText(loghelperFilePath, loghelperContent);

            foreach (var tinfo in tables)
            {
                DbTableCodeGen accessgen = new DbTableCodeGen();
                //model.cs
                string modelFilePath = Path.Combine(testappFolder, string.Format(@"Model\{0}.cs", tinfo.PascalName));
                string modelContent = accessgen.GenModelCode(tinfo);
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

                //add file to proj
                model.Files.Add(string.Format(@"Model\{0}.cs", tinfo.PascalName));
                model.Files.Add(string.Format(@"DAL\{0}Access.cs", tinfo.PascalName));
            }

            foreach (var vinfo in views)
            {
                DbViewCodeGen accessgen = new DbViewCodeGen();
                //model.cs
                string modelFilePath = Path.Combine(testappFolder, string.Format(@"Model\{0}.cs", vinfo.PascalName));
                string modelContent = accessgen.GenModelCode(vinfo);
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

                //add file to proj
                model.Files.Add(string.Format(@"Model\{0}.cs", vinfo.PascalName));
                model.Files.Add(string.Format(@"DAL\{0}Access.cs", vinfo.PascalName));
            }

            //testapp csproj
            string csprojFilePath = Path.Combine(testappFolder, string.Format("{0}.csproj", model.ProjectName));
            string csprojContent = codeGen.Gen(this.TempPath_ConsoleProj_CSProj, model);
            File.WriteAllText(csprojFilePath, csprojContent);

            return root;
        }
    }
}
