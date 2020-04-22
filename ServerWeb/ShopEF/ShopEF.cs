﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShopEF
{
    class ShopEF
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
                .UseSqlServer(connectionString)
                .Options;

            using (var db = new ShopContext(options))
            {
                var category1 = new Category { Name = "Fruits" };
                var category2 = new Category { Name = "Vegetables" };
                var category3 = new Category { Name = "MilkProducts" };
                var category4 = new Category { Name = "Food" };
                db.Categories.AddRange(new List<Category> { category1, category2, category3, category4 });
                db.SaveChanges();

                var product1 = new Product { Name = "Apple", Price = 69 };
                var product2 = new Product { Name = "Orange", Price = 82 };
                var product3 = new Product { Name = "Cucumber", Price = 120 };
                var product4 = new Product { Name = "Potato", Price = 34 };
                var product5 = new Product { Name = "Milk", Price = 72 };
                var product6 = new Product { Name = "Cheese", Price = 234 };
                db.Products.AddRange(new List<Product> { product1, product2, product3, product4, product5, product6 });
                db.SaveChanges();

                product1.ProductCategories.Add(new ProductCategory { ProductId = product1.Id, CategoryId = category1.Id });
                product1.ProductCategories.Add(new ProductCategory { ProductId = product1.Id, CategoryId = category4.Id });
                product2.ProductCategories.Add(new ProductCategory { ProductId = product2.Id, CategoryId = category1.Id });
                product2.ProductCategories.Add(new ProductCategory { ProductId = product2.Id, CategoryId = category4.Id });
                product3.ProductCategories.Add(new ProductCategory { ProductId = product3.Id, CategoryId = category2.Id });
                product3.ProductCategories.Add(new ProductCategory { ProductId = product3.Id, CategoryId = category4.Id });
                product4.ProductCategories.Add(new ProductCategory { ProductId = product4.Id, CategoryId = category2.Id });
                product4.ProductCategories.Add(new ProductCategory { ProductId = product4.Id, CategoryId = category4.Id });
                product5.ProductCategories.Add(new ProductCategory { ProductId = product5.Id, CategoryId = category3.Id });
                product5.ProductCategories.Add(new ProductCategory { ProductId = product5.Id, CategoryId = category4.Id });
                product6.ProductCategories.Add(new ProductCategory { ProductId = product6.Id, CategoryId = category3.Id });
                product6.ProductCategories.Add(new ProductCategory { ProductId = product6.Id, CategoryId = category4.Id });
                db.SaveChanges();

                var customer1 = new Customer { FullName = "Ivanov Ivan Ivanovich", PhoneNumber = "8-900-123-45-67", Email = "ivanov@gmail.com" };
                var customer2 = new Customer { FullName = "Alexandrova Alexandra Alexandrovna", PhoneNumber = "8-900-765-43-21", Email = "alexandrova@gmail.com" };
                db.Customers.AddRange(new List<Customer> { customer1, customer2 });
                db.SaveChanges();

                var order1 = new Order { Date = "21.21.19", Customer = customer1 };
                var order2 = new Order { Date = "01.01.20", Customer = customer2 };
                db.Orders.AddRange(new List<Order> { order1, order2 });
                db.SaveChanges();

                product1.ProductOrders.Add(new ProductOrder { ProductId = product1.Id, OrderId = order1.Id });
                product1.ProductOrders.Add(new ProductOrder { ProductId = product1.Id, OrderId = order2.Id });
                product2.ProductOrders.Add(new ProductOrder { ProductId = product2.Id, OrderId = order2.Id });
                product3.ProductOrders.Add(new ProductOrder { ProductId = product3.Id, OrderId = order1.Id });
                product4.ProductOrders.Add(new ProductOrder { ProductId = product4.Id, OrderId = order2.Id });
                product5.ProductOrders.Add(new ProductOrder { ProductId = product5.Id, OrderId = order1.Id });
                product6.ProductOrders.Add(new ProductOrder { ProductId = product6.Id, OrderId = order2.Id });
                db.SaveChanges();

                // __________________________________________________________________________________________________________

                var orders1 = db.Orders.Include(o => o.ProductOrders).ThenInclude(po => po.Product).ToList();

                Console.WriteLine("Продукты дороже 100 рублей с датой заказа: ");

                foreach (var o in orders1)
                {
                    Console.WriteLine($"Дата закака: {o.Date}");

                    var products1 = o.ProductOrders.Select(po => po.Product).Where(p => p.Price > 100).ToList();

                    foreach (var p in products1)
                    {
                        Console.WriteLine($"{p.Name} {p.Price}");
                    }
                }

                Console.WriteLine("__________________________________________");
                Console.WriteLine();

                // __________________________________________________________________________________________________________

                Console.WriteLine("Цена товара до редактирования:");

                var product = db.Products.FirstOrDefault(p => p.Id == 1);

                Console.WriteLine($"{product.Name} {product.Price}");

                db.Products.FirstOrDefault(p => p.Id == 1).Price = 77;
                db.SaveChanges();

                var productAfterPriceReview = db.Products.FirstOrDefault(p => p.Id == 1);
                Console.WriteLine("Цена товара после редактирования:");
                Console.WriteLine($"{productAfterPriceReview.Name} {productAfterPriceReview.Price}");

                Console.WriteLine("__________________________________________");
                Console.WriteLine();

                // __________________________________________________________________________________________________________

                var products2 = db.Products.Select(p => p.Name).ToList();

                Console.WriteLine("Товары в наличии:");

                foreach (var p in products2)
                {
                    Console.WriteLine(p);
                }

                var removedProduct = db.Products.FirstOrDefault(p => p.Name == "Milk");

                db.Products.Remove(removedProduct);
                db.SaveChanges();

                var productsAfterSale = db.Products.Select(p => p.Name).ToList();

                Console.WriteLine("Товары в наличии после распродажи:");

                foreach (var p in productsAfterSale)
                {
                    Console.WriteLine(p);
                }

                Console.WriteLine("__________________________________________");
                Console.WriteLine();

                // __________________________________________________________________________________________________________

                var products3 = db.Products.Include(p => p.ProductOrders).ToList();
                var productsIds = new List<int>();

                foreach (var r in products3)
                {
                    productsIds.AddRange(r.ProductOrders.Select(p => p.ProductId));
                }

                var productId = productsIds.GroupBy(p => p).OrderByDescending(o => o.Count()).FirstOrDefault().Key;

                Console.WriteLine("Самый часто покупаемый товар:");

                var productName = db.Products.FirstOrDefault(p => p.Id == productId).Name;

                Console.WriteLine(productName);

                Console.WriteLine("__________________________________________");
                Console.WriteLine();

                // __________________________________________________________________________________________________________

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

                // __________________________________________________________________________________________________________

                var categories = db.Categories.Include(c => c.ProductCategories).ThenInclude(pc => pc.Product)
                    .ThenInclude(p => p.ProductOrders);

                foreach (var c in categories)
                {
                    Console.WriteLine($"Категория: {c.Name}");

                    var products = c.ProductCategories;
                    var productsCount = products.Select(p => p.Product.ProductOrders).Select(o => o.Count).Sum();

                    Console.WriteLine($"Продуктов куплено: {productsCount}");
                    Console.WriteLine();
                }
            }

            Console.Read();
        }
    }
}
