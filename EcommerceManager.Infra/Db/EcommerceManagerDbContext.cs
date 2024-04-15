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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().OwnsOne(p => p.Dimensions, r => 
            {
                r.Property(o => o.Width).HasColumnName("width");
                r.Property(o => o.Height).HasColumnName("height");
                r.Property(o => o.Length).HasColumnName("length");

            });

            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Image).HasMaxLength(400);
            modelBuilder.Entity<Product>().Property(p => p.Colour).HasMaxLength(20);
            modelBuilder.Entity<Product>().Property(p => p.SKU).HasMaxLength(20);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
