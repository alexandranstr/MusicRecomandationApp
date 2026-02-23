using Microsoft.EntityFrameworkCore;
using MusicRecApp.Model; 

namespace MusicRecApp.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteSong> Favorites { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("Data Source=music.db");
            }
        }
    }
}