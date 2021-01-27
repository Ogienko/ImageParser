using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using ImageParser.Lib.Extensions;
using ImageParser.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageParser.Lib
{
    /// <summary>
    /// Парсер изображений
    /// </summary>
    public class Parser : IDisposable
    {
        private readonly HttpClient HttpClient;

        private readonly string _imgTag = "img";

        public Parser()
        {
            HttpClient = new HttpClient();
        }

        /// <summary>
        /// Получить все изображения с URL-адреса
        /// </summary>
        /// <param name="url">URL-адрес</param>
        /// <returns>Массив изображений</returns>
        public async Task<Image[]> GetImagesAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException(nameof(url));

            var sources = await GetSourcesAsync(url);

            var items = await DownloadMultipleImagesAsync(sources);

            var images = items
                .Select((item, index) => new Image
                {
                    Name = sources[index].CutFromLastSlash(),
                    Bytes = item
                })
                .ToArray();

            return images;
        }

        /// <summary>
        /// Получить все img-источники c URL-адреса
        /// </summary>
        /// <param name="url">URL-адрес</param>
        /// <returns>Массив img-источников</returns>
        private async Task<string[]> GetSourcesAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException(nameof(url));

            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);
            using var document = await context.OpenAsync(url);
            var sources = document
                .QuerySelectorAll<IHtmlImageElement>(_imgTag)
                .Where(el => !string.IsNullOrEmpty(el.Source))
                .Select(el => el.Source)
                .ToArray();

            return sources;
        }

        /// <summary>
        /// Скачать изображения
        /// </summary>
        /// <param name="sources">URL-адреса</param>
        /// <returns>Изображения в байтах</returns>
        private async Task<byte[][]> DownloadMultipleImagesAsync(IEnumerable<string> sources)
        {
            if (sources == default(IEnumerable<string>))
                throw new ArgumentException(nameof(sources));

            return await Task.WhenAll(sources.Select(s => DownloadImageAsync(s)));
        }

        /// <summary>
        /// Скачать изображение
        /// </summary>
        /// <param name="url">URL-адрес</param>
        /// <returns>Изображение в байтах</returns>
        private async Task<byte[]> DownloadImageAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException(nameof(url));

            return await HttpClient.GetByteArrayAsync(url);
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
