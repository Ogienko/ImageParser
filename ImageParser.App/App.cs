using ImageParser.App.Entities;
using ImageParser.App.Interfaces;
using ImageParser.Lib;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImageParser.App
{
    public class App
    {
        private readonly ISaverService _saverService;

        public App(ISaverService saverService)
        {
            _saverService = saverService;
        }

        public async Task RunAsync()
        {
            try
            {
                Console.Write("Введите URL-адрес: ");
                var url = Console.ReadLine();

                Console.WriteLine("Пожалуйста подождите");

                using var parser = new Parser();

                var images = await parser.GetImagesAsync(url);

                Console.WriteLine($"Получено {images.Length} изображений");

                var imgFiles = images
                    .Select(img => new ImgFile
                    {
                        Name = img.Name,
                        Bytes = img.Bytes
                    })
                    .ToArray();

                await _saverService.SaveAsync(imgFiles);

                Console.WriteLine("Готово");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
