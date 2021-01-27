using System;

namespace ImageParser.Lib.Models
{
    /// <summary>
    /// Изображение
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержимое в байтах
        /// </summary>
        public byte[] Bytes { get; set; }
    }
}
