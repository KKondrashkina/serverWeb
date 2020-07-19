using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopMigration.DataAccess;

namespace ShopMigration
{
    class ShopMigration
    {
        static void Main(string[] args)
        {
            using (var db = new ShopContext())
            {
                db.Database.Migrate();

                var customers = db.Customers
                    .Include(c => c.Orders)
                    .ThenInclude(o => o.ProductOrders)
                    .ThenInclude(po => po.Product)
                    .ToList();

                foreach (var c in customers)
                {
                    Console.WriteLine($"Покупатель: {c.FullName}");

                    var sum = 0;
                    var orders = c.Orders
                        .Select(p => p.ProductOrders)
                        .SelectMany(order => order)
                        .ToList();

                    foreach (var p in orders)
                    {
                        var price = p.Product.Price;
                        var count = p.ProductCount;
                        sum += price * count;
                    }

                    Console.WriteLine($"Потрачено денег: {sum} рубля");
                    Console.WriteLine();
                }
            }
        }
    }
}
