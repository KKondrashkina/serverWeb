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
                var orders2 = db.Orders.Include(o => o.ProductOrders).ThenInclude(po => po.Product).Include(c => c.Customer).ToList(); ;

                foreach (var o in orders2)
                {
                    Console.WriteLine($"Покупатель: {o.Customer.FullName}");

                    var products1 = o.ProductOrders.Select(po => po.Product).Select(p => p.Price).ToList();
                    var moneySpent = products1.Sum();

                    Console.WriteLine($"Потрачено денег: {moneySpent} рубля");
                    Console.WriteLine();
                }

                Console.WriteLine("__________________________________________");
                Console.WriteLine();
            }
        }
    }
}
