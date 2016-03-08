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
        public static char[] InvalidToken = new char[] {
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
            string[] splittedPhrase = phrase.Split(InvalidToken);
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
            string[] splittedPhrase = phrase.Split(InvalidToken);
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
            string[] splittedPhrase = phrase.Split(InvalidToken);
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
    }
}
