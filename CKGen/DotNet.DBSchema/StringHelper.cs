// -----------------------------------------------------------------------
// <copyright file="StringHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.DBSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class StringHelper
    {
        /// <summary>
        /// 无效字符串验证
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static bool InvalidString(object srcString)
        {
            return (((srcString == DBNull.Value) || (srcString == null)) || (srcString.ToString().Trim() == string.Empty));
        }

        /// <summary>
        /// columnName
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string SetCamelCase(string phrase)
        {
            string[] splittedPhrase = phrase.Split(' ', '-', '.');
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
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string SetPascalCase(string phrase)
        {
            string[] splittedPhrase = phrase.Split(' ', '-', '.');
            var sb = new StringBuilder();

            //sb = new StringBuilder();

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
            string[] splittedPhrase = phrase.Split(' ', '-', '.');
            var sb = new StringBuilder();

            //sb = new StringBuilder();

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
