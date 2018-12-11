using System;

namespace Chat2Desk.Utils.Extensions
{
    /// <summary>
    /// Расширяющий класс для String
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Заменяет символ в строке на новый символ
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="index">The index.</param>
        /// <param name="newChar">The new character.</param>
        /// <returns></returns>
        public static string ReplaceCharInString(this string str, int index, Char newChar)
        {
            return str.Remove(index, 1).Insert(index, newChar.ToString());
        }

        /// <summary>
        /// Заменяет символ в строке на новый символ
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="index">The index.</param>
        /// <param name="newString">The new string.</param>
        /// <returns></returns>
        public static string ReplaceCharInString(this string str, int index, string newString)
        {
            return str.Remove(index, 1).Insert(index, newString);
        }
    }
}