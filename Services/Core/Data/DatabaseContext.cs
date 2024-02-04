using Aurora.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data
{
    /// <summary>
    /// The database context for Entity Framework Core.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// All the projects in the database.
        /// </summary>
        public DbSet<Project> Projects { get; set; } = null!;
        /// <summary>
        /// All the accounts in the database.
        /// </summary>
        public DbSet<Account> Accounts { get; set; } = null!;

        /// <summary>
        /// Configure the database context.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Database=aurora;Username=test;Password=test");
        }
    }
}
