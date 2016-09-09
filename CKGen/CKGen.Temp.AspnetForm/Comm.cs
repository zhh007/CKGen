using CKGen.Base.Form;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace CKGen.Temp.AspnetForm
{
    public class Comm
    {
        public static string GetTemplete(string tmp)
        {
            string tmpName = string.Format("CKGen.Temp.AspnetForm.Template.{0}", tmp);
            Assembly myAssembly = typeof(Comm).Assembly;
            using (Stream input = myAssembly.GetManifestResourceStream(tmpName))
            using (StreamReader reader = new StreamReader(input, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GetConvertStringFromRequest(ModuleField field)
        {
            string result = "";

            if (LanguageConvert.IsValueType(field.LanguageType))
            {
                result = string.Format("({1})Request.Form[\"{0}\"]", field.FieldName, field.LanguageType);
            }
            else
            {
                result = string.Format("Request.Form[\"{0}\"] as {1}", field.FieldName, field.LanguageType);
            }

            return result;
        }

        public static string GetNullCheckString(ModuleField field, string varname)
        {
            string result = "";

            if (field.LanguageType == "string")
            {
                result = string.Format("string.IsNullOrEmpty({0})", varname);
            }
            else
            {
                if (LanguageConvert.IsValueType(field.LanguageType))
                {
                    //result = string.Format("({1})Request.Form[\"{0}\"]", field.FieldName, field.LanguageType);
                }
                else
                {
                    result = string.Format("{1} == null", varname);
                }
            }

            return result;
        }
    }
}
