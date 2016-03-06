using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace CKGen.Temp.Adonet
{
    public class Comm
    {
        public static string GetTemplete(string tmp)
        {
            string tmpName = string.Format("CKGen.Temp.Adonet.Template.{0}", tmp);
            Assembly myAssembly = typeof(Comm).Assembly;
            using (Stream input = myAssembly.GetManifestResourceStream(tmpName))
            using (StreamReader reader = new StreamReader(input, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
