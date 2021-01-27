using System;

namespace ImageParser.Lib.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Вырезать строку от последнего слеша
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns>Строка от последнего слеша</returns>
        public static string CutFromLastSlash(this string str)
        {
            if (str == default(string))
                throw new ArgumentNullException(nameof(str));

            var index = str.LastIndexOf('/');

            return index == -1 ? str : str.Substring(index + 1);
        }
    }
}
