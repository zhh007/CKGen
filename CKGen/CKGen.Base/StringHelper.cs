// -----------------------------------------------------------------------
// <copyright file="StringHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CKGen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class StringHelper
    {
        public static char[] InvalidSymbol = new char[] {
            ' ', ',', '.', '?', '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '[', ']', '{', '}', '\\', '|', ':', ';', '"', '\'', '/', '<', '>',
            '　', '，', '。', '？', '·', '！', '￥', '%', '…', '&', '*', '（', '）', '—', '【', '】', '、', '|', '：', '；', '“', '”', '‘', '’', '《', '》',
        };

        /// <summary>
        /// 无效字符串验证
        /// </summary>
        public static bool InvalidString(object srcString)
        {
            return (((srcString == DBNull.Value) || (srcString == null)) || (srcString.ToString().Trim() == string.Empty));
        }

        /// <summary>
        /// columnName
        /// </summary>
        public static string SetCamelCase(string phrase)
        {
            string[] splittedPhrase = phrase.Split(InvalidSymbol);
            var sb = new StringBuilder();

            sb.Append(splittedPhrase[0].ToLower());
            splittedPhrase[0] = string.Empty;

            foreach (String s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                if (splittedPhraseChars.Length > 0)
                {
                    splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                }
                sb.Append(new String(splittedPhraseChars));
            }
            return sb.ToString();
        }

        /// <summary>
        /// ColumnName
        /// </summary>
        public static string SetPascalCase(string phrase)
        {
            string[] splittedPhrase = phrase.Split(InvalidSymbol);
            var sb = new StringBuilder();

            foreach (String s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                if (splittedPhraseChars.Length > 0)
                {
                    splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                }
                sb.Append(new String(splittedPhraseChars));
            }
            return sb.ToString();
        }

        public static string SetValidName(string phrase)
        {
            string[] splittedPhrase = phrase.Split(InvalidSymbol);
            var sb = new StringBuilder();

            foreach (String s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                //if (splittedPhraseChars.Length > 0)
                //{
                //    splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                //}
                sb.Append(new String(splittedPhraseChars));
            }
            return sb.ToString();
        }

        public static string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append("\"");

            return sb.ToString();
        }
    }
}
