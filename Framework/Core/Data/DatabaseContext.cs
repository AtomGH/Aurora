using Framework.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Database=aurora;Username=test;Password=test");
        }
    }
}
