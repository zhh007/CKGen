// -----------------------------------------------------------------------
// <copyright file="SolutionBuilder.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen.Temp.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.CodeDom.Compiler;
    using Microsoft.CSharp;
    using System.IO;
    using DBSchema;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SolutionBuilder
    {
        public string SolutionName
        {
            get;
            private set;
        }

        public string DBConnectionString
        {
            get;
            private set;
        }

        public string DataBaseName
        {
            get;
            private set;
        }

        private IDatabaseInfo _database;

        private Guid solutionID;
        private Guid entitiesProjID;
        private Guid testProjID;
        private string solutionFolder;
        private string entitiesProjectFolder;
        private string entitiesProjName;
        private string testProjectFolder;
        private string testProjName;

        public SolutionBuilder(IDatabaseInfo database, string solutionName, string dbname, string dbconnStr)
        {
            this._database = database;
            this.SolutionName = solutionName;
            this.DataBaseName = dbname;
            this.DBConnectionString = dbconnStr;

            this.solutionID = Guid.NewGuid();
            this.entitiesProjID = Guid.NewGuid();
            this.testProjID = Guid.NewGuid();

            this.solutionFolder = System.IO.Path.Combine(Environment.CurrentDirectory, this.SolutionName);

            this.entitiesProjName = solutionName + ".DataAccess";
            this.entitiesProjectFolder = System.IO.Path.Combine(solutionFolder, this.entitiesProjName);

            this.testProjName = solutionName + ".Test";
            this.testProjectFolder = System.IO.Path.Combine(solutionFolder, this.testProjName);

            if (Directory.Exists(this.solutionFolder))
            {
                Directory.Delete(this.solutionFolder, true);
            }
            Directory.CreateDirectory(this.solutionFolder);
        }

        public bool Build(ref string errorStr)
        {
            bool result = false;

            string solutionFilePath = System.IO.Path.Combine(solutionFolder, this.SolutionName + ".sln");

            string referencePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Reference");
            string targetRefPath = System.IO.Path.Combine(solutionFolder, "Reference");

            string entitiesProjectFilePath = System.IO.Path.Combine(entitiesProjectFolder, this.entitiesProjName + ".csproj");
            string dllFilePath = System.IO.Path.Combine(solutionFolder, this.DataBaseName + ".DataAccess.dll");
            string xmlDocPath = System.IO.Path.Combine(solutionFolder, this.DataBaseName + ".DataAccess.xml");

            //string docRootPath = System.IO.Path.Combine(solutionFolder, "docs_temp");
            //DBDocBuilder.DBDocBuilder docBuilder = new DBDocBuilder.DBDocBuilder(this.DBConnectionString, this.DataBaseName, docRootPath);
            //docBuilder.Build();



            EntitiesBuilder eb = new EntitiesBuilder(this._database, this.DataBaseName, this.SolutionName, entitiesProjectFolder);
            List<string> filePaths = new List<string>();
            filePaths = eb.Build();
            //
            IDictionary<String, String> providerOptions = new Dictionary<String, String>();
            providerOptions["CompilerVersion"] = "v3.5";

            string publishKeyPath = Path.Combine(Environment.CurrentDirectory, "publishKey.snk");

            CodeDomProvider codeDomProvider = new CSharpCodeProvider(providerOptions);

            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");//引用dll
            objCompilerParameters.ReferencedAssemblies.Add("System.Data.dll");//引用dll
            objCompilerParameters.ReferencedAssemblies.Add("System.Configuration.dll");//引用dll
            objCompilerParameters.ReferencedAssemblies.Add("System.Xml.dll");//引用dll

            objCompilerParameters.ReferencedAssemblies.Add(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "zlab.dll"));//引用dll
            objCompilerParameters.ReferencedAssemblies.Add(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "zlab.ORM.dll"));//引用dll

            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = false;
            objCompilerParameters.WarningLevel = 3;
            objCompilerParameters.TreatWarningsAsErrors = false;
            objCompilerParameters.CompilerOptions = "/optimize /keyfile:" + publishKeyPath + " /doc:" + xmlDocPath;
            //objCompilerParameters.TempFiles = new TempFileCollection(".", true);
            objCompilerParameters.OutputAssembly = dllFilePath;



            if (!solutionFolder.EndsWith("\\"))
            {
                solutionFolder += "\\" + entitiesProjName + "\\";
            }
            else
            {
                solutionFolder += entitiesProjName + "\\";
            }
            List<string> tmpFiles = new List<string>();
            foreach (string f in filePaths)
            {
                string fpath = f.Replace(solutionFolder, "");
                tmpFiles.Add(fpath);
            }
            EntitiesProjectTemplate entitiesProjTemplete = new EntitiesProjectTemplate(this.entitiesProjID, this.entitiesProjName, tmpFiles);
            File.AppendAllText(entitiesProjectFilePath, entitiesProjTemplete.TransformText());


            slnTemplate slntmp = new slnTemplate(this.solutionID, this.entitiesProjName, this.entitiesProjID, this.testProjName, this.testProjID);
            File.AppendAllText(solutionFilePath, slntmp.TransformText());

            //copy reference folder
            //CopyDir.Copy(referencePath, targetRefPath);

            //创建测试项目
            CreateTestProj();

            CompilerResults cr = codeDomProvider.CompileAssemblyFromFile(objCompilerParameters, filePaths.ToArray<string>());

            //if (cr.Errors.HasErrors)
            //{
            //    result = false;
            //    errorStr = "编译错误：\r\n";
            //    foreach (CompilerError err in cr.Errors)
            //    {
            //        string line = "File Name:" + err.FileName + "Line:" + err.Line +
            //            ", Error Number: " + err.ErrorNumber +
            //            ", '" + err.ErrorText + ";" + Environment.NewLine;

            //        errorStr += line;
            //    }
            //}
            //else
            //{
            //    result = true;
            //    //// 通过反射，调用HelloWorld的实例
            //    // Assembly objAssembly = cr.CompiledAssembly;
            //    //object objHelloWorld = objAssembly.CreateInstance("DynamicCodeGenerate.HelloWorld");
            //    // MethodInfo objMI = objHelloWorld.GetType().GetMethod("OutPut");
            //    //// 调用执行
            //    // Console.WriteLine(objMI.Invoke(objHelloWorld, null));
            //}

            return result;
        }

        private void CreateTestProj()
        {
            if (!Directory.Exists(this.testProjectFolder))
            {
                Directory.CreateDirectory(this.testProjectFolder);
            }

            TestProj.csprojTemplate csprojTemplete = new TestProj.csprojTemplate(this.entitiesProjName, this.entitiesProjID, this.testProjName, this.testProjID);
            string testProjFilePath = System.IO.Path.Combine(this.testProjectFolder, this.testProjName + ".csproj");
            string csprojCode = csprojTemplete.TransformText();
            File.AppendAllText(testProjFilePath, csprojCode);

            TestProj.ProgramTemplete progTemplete = new TestProj.ProgramTemplete(this.testProjName);
            string tmpCode = progTemplete.TransformText();
            File.AppendAllText(System.IO.Path.Combine(this.testProjectFolder, "Program.cs"), tmpCode);

            TestProj.AssemblyInfoTemplate assTemplete = new TestProj.AssemblyInfoTemplate();
            string assCode = assTemplete.TransformText();
            Directory.CreateDirectory(System.IO.Path.Combine(this.testProjectFolder, "Properties"));
            File.AppendAllText(System.IO.Path.Combine(this.testProjectFolder, "Properties\\AssemblyInfo.cs"), assCode);

            TestProj.AppConfigTemplate appConfigTemplete = new TestProj.AppConfigTemplate(this.DataBaseName, this.DBConnectionString);
            string appConfigCode = appConfigTemplete.TransformText();
            File.AppendAllText(System.IO.Path.Combine(this.testProjectFolder, "App.config"), appConfigCode);
        }
    }
}
