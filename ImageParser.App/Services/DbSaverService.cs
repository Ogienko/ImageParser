using ImageParser.App.DbContexts;
using ImageParser.App.Entities;
using ImageParser.App.Interfaces;
using System;
using System.Threading.Tasks;

namespace ImageParser.App.Services
{
    public class DbSaverService : ISaverService
    {
        private readonly AppDbContext _context;

        public DbSaverService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(ImgFile[] imgFiles)
        {
            if (imgFiles == default(ImgFile[]))
                throw new ArgumentNullException(nameof(imgFiles));

            await _context.ImgFiles.AddRangeAsync(imgFiles);
            await _context.SaveChangesAsync();
        }
    }
}
