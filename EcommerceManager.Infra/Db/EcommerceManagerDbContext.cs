using EcommerceManager.Domain.Models;
using EcommerceManager.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManager.Db
{
    public class EcommerceManagerDbContext : DbContext
    {
        public EcommerceManagerDbContext(DbContextOptions<EcommerceManagerDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
