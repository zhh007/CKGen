﻿using CKGen.Base;
using CKGen.DBSchema;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CKGen.Services
{
    public class CodeGenService : ICodeGenService
    {
        public string GenByPath(string viewpath, object model)
        {
            string viewname = System.IO.Path.GetFileName(viewpath);
            string tmp = System.IO.File.ReadAllText(viewpath);
            return RazorService.Instance.Gen(tmp, model);
        }

        public void GenSave(string viewpath, object model, string filepath)
        {
            string viewname = System.IO.Path.GetFileName(viewpath);
            string tmp = System.IO.File.ReadAllText(viewpath);
            string result = RazorService.Instance.Gen(tmp, model);

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

        public string Gen(string tmp, object model)
        {
            return RazorService.Instance.Gen(tmp, model);
        }
    }

    internal class RazorService
    {
        private readonly IRazorEngineService _service;
        private static RazorService instance = new RazorService();
        public static RazorService Instance
        {
            get
            {
                return instance;
            }
        }
        private RazorService()
        {
            TemplateServiceConfiguration config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true; // loads the files in-memory (gives the templates full-trust permissions)
            config.CachingProvider = new DefaultCachingProvider(t => { }); //disables the warnings
            config.EncodedStringFactory = new RazorEngine.Text.RawStringFactory(); // Raw string encoding.

            // Use the config
            _service = RazorEngineService.Create(config); // new API
        }

        public string Gen(string tmp, object model)
        {
            string viewname = MD5(tmp);
            return _service.RunCompile(tmp, viewname, model.GetType(), model);
        }

        public string MD5(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

}
