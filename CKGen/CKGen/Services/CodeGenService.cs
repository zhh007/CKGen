using CKGen.Base;
using CKGen.DBSchema;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CKGen.Services
{
    public class CodeGenService : ICodeGenService
    {
        public string Gen(string viewpath, object model)
        {
            string viewname = System.IO.Path.GetFileName(viewpath);
            string tmp = System.IO.File.ReadAllText(viewpath);
            return GetRazor().RunCompile(tmp, viewname, model.GetType(), model);
        }

        public void GenSave(string viewpath, object model, string filepath)
        {
            string viewname = System.IO.Path.GetFileName(viewpath);
            string tmp = System.IO.File.ReadAllText(viewpath);
            string result = GetRazor().RunCompile(tmp, viewname, model.GetType(), model);

            string dir = System.IO.Path.GetPathRoot(filepath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (var sw = new StreamWriter(File.Open(filepath, FileMode.CreateNew), System.Text.Encoding.UTF8))
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
