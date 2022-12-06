using Microsoft.EntityFrameworkCore;
using ProductApplication.Models.Domain;

namespace ProductApplication.Data
{
    public class PUserDbContext : DbContext
    {

        public PUserDbContext(DbContextOptions<PUserDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
