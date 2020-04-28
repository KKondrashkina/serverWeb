using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopMigration.DataAccess;

namespace ShopMigration
{
    class Program
    {
        static void Main(string[] args)
        {
           var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString,x => x.MigrationsAssembly("ShopMigration.DataAccess"))
                .Options;

            using (var db = new ShopContext(options))
            {
                /*var categories = db.Categories.Include(c => c.ProductCategories).ThenInclude(pc => pc.Product)
                    .ThenInclude(p => p.ProductOrders);

                foreach (var c in categories)
                {
                    Console.WriteLine($"Категория: {c.Name}");

                    var products = c.ProductCategories;
                    var productsCount = products.Select(p => p.Product.ProductOrders).Select(o => o.Count).Sum();

                    Console.WriteLine($"Продуктов куплено: {productsCount}");
                    Console.WriteLine();
                }*/
            }
        }
    }
}
