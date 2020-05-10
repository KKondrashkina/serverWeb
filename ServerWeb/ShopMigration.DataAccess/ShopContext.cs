using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopMigration.DataAccess.Model;

namespace ShopMigration.DataAccess
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("ShopMigration.DataAccess"));
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOrder>()
                .HasKey(k => new { k.ProductId, k.OrderId });

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.ProductId);

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Order)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(po => po.OrderId);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(k => new { k.ProductId, k.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(po => po.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);
            modelBuilder.Entity<Category>()
                    .Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(p => p.FullName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(p => p.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(p => p.Email)
                .HasMaxLength(50);

            modelBuilder.Entity<Order>()
                .Property(p => p.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Product>()
               .Property(p => p.Price)
               .IsRequired();

            modelBuilder.Entity<ProductCategory>()
                .Property(p => p.ProductId)
                .IsRequired();

            modelBuilder.Entity<ProductCategory>()
                .Property(p => p.CategoryId)
                .IsRequired();

            modelBuilder.Entity<ProductOrder>()
               .Property(p => p.ProductId)
               .IsRequired();

            modelBuilder.Entity<ProductOrder>()
                .Property(p => p.OrderId)
                .IsRequired();

            modelBuilder.Entity<ProductOrder>()
                .Property(p => p.ProductCount)
                .IsRequired();
        }
    }
}
