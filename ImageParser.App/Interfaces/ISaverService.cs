using ImageParser.App.Entities;
using System.Threading.Tasks;

namespace ImageParser.App.Interfaces
{
    public interface ISaverService
    {
        /// <summary>
        /// Сохранить файлы изображений
        /// </summary>
        /// <param name="imgFiles">Файлы изображений</param>
        /// <returns></returns>
        public Task SaveAsync(ImgFile[] imgFiles);
    }
}
