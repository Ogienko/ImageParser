using ImageParser.App.Entities;
using ImageParser.App.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageParser.App.Services
{
    public class FileSaverService : ISaverService
    {
        public async Task SaveAsync(ImgFile[] imgFiles)
        {
            if (imgFiles == default(ImgFile[]))
                throw new ArgumentNullException(nameof(imgFiles));

            foreach (var imgFile in imgFiles)
            {
                await File.WriteAllBytesAsync(imgFile.Name, imgFile.Bytes);
            }
        }
    }
}
