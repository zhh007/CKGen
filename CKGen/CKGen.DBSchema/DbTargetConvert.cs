using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Diagnostics;

namespace CKGen.DBSchema
{
    public class DbTargetConvert
    {
        private static Dictionary<KeyValuePair<string, string>, Dictionary<string, string>> innerDict = new Dictionary<KeyValuePair<string, string>, Dictionary<string, string>>();

        private DbTargetConvert()
        {
        }

        private string GetTargetType(string f, string t, string sourceType)
        {
            KeyValuePair<string, string> key = new KeyValuePair<string, string>(f, t);

            if (!innerDict.ContainsKey(key))
            {
                Stream resourceStream = Assembly.GetAssembly(this.GetType()).GetManifestResourceStream("CKGen.DBSchema.Res.DbTargets.xml");

                if (resourceStream != null)
                {
                    StreamReader resourceStreamReader = new StreamReader(resourceStream);
                    TextReader tr = new StringReader(resourceStreamReader.ReadToEnd());
                    XElement root = XElement.Load(tr, LoadOptions.None);

                    var tests =
                        (from el in root.Elements("DbTarget")
                         where (string)el.Attribute("From") == f & (string)el.Attribute("To") == t
                         select el).First().Descendants("Type");

                    Dictionary<string, string> tmp = new Dictionary<string, string>();
                    foreach (XElement el in tests)
                    {
                        tmp.Add(el.Attribute("From").Value, el.Attribute("To").Value);
                        //Console.WriteLine((string)el.Attribute("TestId"));
                    }

                    innerDict.Add(new KeyValuePair<string, string>(f, t), tmp);
                    //XElement gobalConfig = root.Element("global");

                    //foreach (XElement x in gobalConfig.Elements())
                    //{
                    //    _properties.Add(x.Name.ToString(), x.Value.ToString());
                    //}
                }
                else
                {
                    throw new NullReferenceException();
                }
            }

            Dictionary<string, string> dict = innerDict[key];

            if (dict != null && dict.ContainsKey(sourceType))
                return dict[sourceType];

            return "未知(" + sourceType + ")";

        }

        public static string GetSqlDbType(string sqltype)
        {
            DbTargetConvert convert = new DbTargetConvert();
            return convert.GetTargetType("SQL", "SqlClient", sqltype);
        }
    }
}
