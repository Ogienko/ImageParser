using ImageParser.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageParser.App.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<ImgFile> ImgFiles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
