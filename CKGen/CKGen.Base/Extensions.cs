using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CKGen
{
    public static class Extensions
    {
        //public static void SetAttributeValue(this XElement el, string attr, object value)
        //{
        //    if (el == null)
        //        return;
        //    if (el.Attribute(attr) == null)
        //    {
        //        el.Add(new XAttribute(attr, value));
        //    }
        //    else
        //    {
        //        el.Attribute(attr).SetValue(value);
        //    }
        //}

        private static readonly Dictionary<Type, string> _typeToFriendlyName = new Dictionary<Type, string>
            {
                {typeof(string), "string"},
                {typeof(object), "object"},
                {typeof(bool), "bool"},
                {typeof(byte), "byte"},
                {typeof(char), "char"},
                {typeof(decimal), "decimal"},
                {typeof(double), "double"},
                {typeof(short), "short"},
                {typeof(int), "int"},
                {typeof(long), "long"},
                {typeof(sbyte), "sbyte"},
                {typeof(float), "float"},
                {typeof(ushort), "ushort"},
                {typeof(uint), "uint"},
                {typeof(ulong), "ulong"},
                {typeof(void), "void"}
            };

        public static string GetFriendlyName(this Type type)
        {
            string friendlyName;
            if (_typeToFriendlyName.TryGetValue(type, out friendlyName))
            {
                return friendlyName;
            }

            //friendlyName = type.Name;
            if (type.Name == type.FullName.Replace("System.", ""))
            {
                friendlyName = type.Name;
            }
            else
            {
                friendlyName = type.FullName;
            }

            if (type.IsGenericType)
            {
                int backtick = friendlyName.IndexOf('`');
                if (backtick > 0)
                {
                    friendlyName = friendlyName.Remove(backtick);
                }
                friendlyName += "<";
                Type[] typeParameters = type.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; i++)
                {
                    string typeParamName = typeParameters[i].GetFriendlyName();
                    friendlyName += (i == 0 ? typeParamName : ", " + typeParamName);
                }
                friendlyName += ">";
            }

            if (type.IsArray)
            {
                return type.GetElementType().GetFriendlyName() + "[]";
            }

            return friendlyName;
        }

        public static string ToHexString(this byte[] source)
        {
            if (source == null) return null;
            if (source.Length == 0) return string.Empty;

            return "0x" + BitConverter.ToString(source).Replace("-", "");
        }

        public static byte[] ToHexBytes(this string source)
        {
            if (source == null) return null;
            if (source.Length == 0) return new byte[0];

            if(source.StartsWith("0x"))
            {
                source = source.Substring(2);
            }

            int l = source.Length / 2;
            var b = new byte[l];
            for (int i = 0; i < l; ++i)
            {
                b[i] = Convert.ToByte(source.Substring(i * 2, 2), 16);
            }
            return b;
        }

    }
}
