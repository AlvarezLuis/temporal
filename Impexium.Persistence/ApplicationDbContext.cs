using Impexium.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Impexium.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
