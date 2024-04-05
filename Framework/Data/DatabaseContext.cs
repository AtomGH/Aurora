using Aurora.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Instance> Instances { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseNpgsql("Host=localhost;Database=aurora;Username=postgres;Password=postgres")
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("framework");

            builder.Entity<Instance>().HasKey(i => i.Id);
            builder.Entity<Instance>().Property(i => i.Token);
            builder.Entity<Instance>().Property(i => i.Hostname);
            builder.Entity<Instance>().Property(i => i.LastRefreshTime);

            base.OnModelCreating(builder);
        }
    }
}
