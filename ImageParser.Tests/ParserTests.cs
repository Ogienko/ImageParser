using ImageParser.Lib;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ImageParser.Tests
{
    public class ParserTests
    {
        private readonly string _url = "http://ozon.ru";

        [Fact]
        public async Task GetImagesThrowsAsync()
        {
            // Arrange
            var parser = new Parser();

            // Act
            Func<Task> act = () => parser.GetImagesAsync("");

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task GetImagesNotNullAsync()
        {
            // Arrange
            var parser = new Parser();

            // Act
            var images = await parser.GetImagesAsync(_url);

            // Assert
            Assert.NotNull(images);
        }

        [Fact]
        public async Task GetImagesNotEmptyAsync()
        {
            // Arrange
            var parser = new Parser();

            // Act
            var images = await parser.GetImagesAsync(_url);

            // Assert
            Assert.NotEmpty(images);
        }
    }
}
