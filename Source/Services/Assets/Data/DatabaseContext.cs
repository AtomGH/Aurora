using Assets.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assets.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetTag> AssetTags { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseNpgsql("Host=localhost;Database=aurora;Username=testassets;Password=testassets")
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("assets");

            builder.Entity<Asset>().Property(a => a.Token).HasColumnType("jsonb");

            base.OnModelCreating(builder);
        }
    }
}
