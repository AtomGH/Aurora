using Aurora.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Instance> Instances { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Database=aurora;Username=test;Password=test");
        }
    }
}
