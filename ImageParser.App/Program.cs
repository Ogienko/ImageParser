using ImageParser.App.DbContexts;
using ImageParser.App.Interfaces;
using ImageParser.App.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageParser.App
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = LoadConfiguration();
            var services = ConfigureServices(configuration);
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<App>().RunAsync();
        }

        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        private static IServiceCollection ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            services.AddSingleton(configuration);

            switch (configuration["Settings:SaveTo"])
            {
                case "DB":
                    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration["ConnectionStrings:AppDbContext"]));
                    services.AddScoped<ISaverService, DbSaverService>();
                    break;
                case "File":
                    services.AddScoped<ISaverService, FileSaverService>();
                    break;
                default:
                    throw new Exception("Конфигурация не настроена");
            }

            services.AddScoped<App>();

            return services;
        }
    }
}
