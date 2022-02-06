using Impexium.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Impexium.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to tables  
            modelBuilder.HasCharSet(CharSet.Utf8Mb4);
            modelBuilder.Entity<Product>().ToTable("products");
        }

        public DbSet<Product> Products { get; set; }
    }
}
