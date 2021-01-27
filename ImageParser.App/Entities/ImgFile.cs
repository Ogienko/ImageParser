using System;

namespace ImageParser.App.Entities
{
    /// <summary>
    /// Файл изображения
    /// </summary>
    public class ImgFile
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        public byte[] Bytes { get; set; }
    }
}
