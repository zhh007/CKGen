using CKGen.DBSchema;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen
{
    public class CodeGen
    {
        public static string GenForTable(string viewname, ITableInfo viewmodel)
        {
            string tmp = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "Template\\" + viewname));
            return GetRazor().RunCompile(tmp, viewname, typeof(ITableInfo), viewmodel);
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
