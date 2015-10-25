using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Diagnostics;

namespace DotNet.DBSchema
{
    public class LanguageConvert
    {
        private static Dictionary<KeyValuePair<string, string>, Dictionary<string, string>> innerDict = new Dictionary<KeyValuePair<string, string>, Dictionary<string, string>>();

        private LanguageConvert()
        {
        }

        private string GetTargetType(string f, string t, string sourceType)
        {
            KeyValuePair<string, string> key = new KeyValuePair<string, string>(f, t);

            if (!innerDict.ContainsKey(key))
            {
                Stream resourceStream = Assembly.GetAssembly(this.GetType()).GetManifestResourceStream("DotNet.DBSchema.Res.Languages.xml");

                if (resourceStream != null)
                {
                    StreamReader resourceStreamReader = new StreamReader(resourceStream);
                    TextReader tr = new StringReader(resourceStreamReader.ReadToEnd());
                    XElement root = XElement.Load(tr, LoadOptions.None);

                    var tests =
                        (from el in root.Elements("Language")
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType">datetime,float等T-SQL类型</param>
        /// <returns></returns>
        public static string GetCSharpTypeFromMSSQL(string sourceType)
        {
            LanguageConvert convert = new LanguageConvert();
            return convert.GetTargetType("SQL", "C#", sourceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType">datetime,float等T-SQL类型</param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static string GetCSharpTypeFromMSSQL(string sourceType, bool nullable)
        {
            string retVal = GetCSharpTypeFromMSSQL(sourceType);

            //switch (retVal)
            //{
            //    case "decimal":
            //        if (col.NumericScale == 0)
            //        {
            //            retVal = "int";
            //        }
            //        else
            //        {
            //            retVal = "double";
            //        }
            //        break;
            //}

            if (nullable && (IsValueType(retVal) || retVal == "Guid" || retVal == "DateTime"))
            {
                retVal += "?";
            }

            return retVal;
        }

        /// <summary>
        /// 是否值类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueType(string type)
        {
            bool ret = false;

            switch (type)
            {
                case "sbyte":
                case "byte":
                case "short":
                case "ushort":
                case "int":
                case "uint":
                case "long":
                case "ulong":
                case "char":
                case "float":
                case "double":
                case "bool":
                case "decimal":
                    ret = true;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 是否字符串类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsStringType(string type)
        {
            bool ret = false;

            switch (type)
            {
                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                case "xml":
                    ret = true;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 是否数字类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumericType(string type)
        {
            bool ret = false;

            switch (type)
            {
                case "bigint":
                case "decimal":
                case "float":
                case "int":
                case "money":
                case "numeric":
                case "real":
                case "smallint":
                case "smallmoney":
                case "tinyint":
                    ret = true;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }
    }
}
