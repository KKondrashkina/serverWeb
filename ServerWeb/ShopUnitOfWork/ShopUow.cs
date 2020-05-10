using System;
using System.Collections.Generic;
using System.Linq;
using ShopUnitOfWork.Model;
using ShopUnitOfWork.RepositoryInterfaces;
using ShopUnitOfWork.Uow;

namespace ShopUnitOfWork
{
    class ShopUow
    {
        static void Main(string[] args)
        {
            using (var uow = new UnitOfWork(new ShopContext()))
            {
                var categoryRepository = uow.GetRepository<ICategoryRepository>();

                var category1 = new Category { Name = "Fruits" };
                var category2 = new Category { Name = "Vegetables" };
                var category3 = new Category { Name = "MilkProducts" };
                var category4 = new Category { Name = "Food" };
                categoryRepository.AddRange(new List<Category> { category1, category2, category3, category4 });
                uow.Save();

                var productRepository = uow.GetRepository<IProductRepository>();

                var product1 = new Product { Name = "Apple", Price = 69 };
                var product2 = new Product { Name = "Orange", Price = 82 };
                var product3 = new Product { Name = "Cucumber", Price = 120 };
                var product4 = new Product { Name = "Potato", Price = 34 };
                var product5 = new Product { Name = "Milk", Price = 72 };
                var product6 = new Product { Name = "Cheese", Price = 234 };
                productRepository.AddRange(new List<Product> { product1, product2, product3, product4, product5, product6 });
                uow.Save();

                productRepository.AddCategoryToProduct(product1, category1);
                productRepository.AddCategoryToProduct(product1, category4);
                productRepository.AddCategoryToProduct(product2, category1);
                productRepository.AddCategoryToProduct(product2, category4);
                productRepository.AddCategoryToProduct(product3, category2);
                productRepository.AddCategoryToProduct(product3, category4);
                productRepository.AddCategoryToProduct(product4, category2);
                productRepository.AddCategoryToProduct(product4, category4);
                productRepository.AddCategoryToProduct(product5, category3);
                productRepository.AddCategoryToProduct(product5, category4);
                productRepository.AddCategoryToProduct(product6, category3);
                productRepository.AddCategoryToProduct(product6, category4);
                uow.Save();

                var customerRepository = uow.GetRepository<ICustomerRepository>();

                var customer1 = new Customer { FullName = "Ivanov Ivan Ivanovich", PhoneNumber = "8-900-123-45-67", Email = "ivanov@gmail.com" };
                var customer2 = new Customer { FullName = "Alexandrova Alexandra Alexandrovna", PhoneNumber = "8-900-765-43-21", Email = "alexandrova@gmail.com" };
                customerRepository.AddRange(new List<Customer> { customer1, customer2 });
                uow.Save();

                var orderRepository = uow.GetRepository<IOrderRepository>();
               
                var order1 = new Order { Date = new DateTime(2019, 12, 12), CustomerId = customer1.Id, Customer = customer1 };
                var order2 = new Order { Date = new DateTime(2020, 3, 4), CustomerId = customer2.Id, Customer = customer2 };
                var order3 = new Order { Date = new DateTime(2019, 12, 11), CustomerId = customer1.Id, Customer = customer1 };
                orderRepository.AddRange(new List<Order> { order1, order2, order3 });
                uow.Save();

                productRepository.AddProductToOrder(product1, order1);
                productRepository.AddProductToOrder(product1, order2);
                productRepository.AddProductToOrder(product1, order3);
                productRepository.AddProductToOrder(product2, order2);
                productRepository.AddProductToOrder(product3, order1);
                productRepository.AddProductToOrder(product4, order2);
                productRepository.AddProductToOrder(product5, order1);
                productRepository.AddProductToOrder(product6, order2);
                uow.Save();

                // __________________________________________________________________________________________________________

                var orders1 = orderRepository.GetOrdersWithProducts();

                Console.WriteLine("Продукты дороже 100 рублей с датой заказа: ");

                foreach (var o in orders1)
                {
                    Console.WriteLine($"Дата закака: {o.Date}");

                    var products1 = orderRepository.GetOrderProductsMoreExpensiveThan(100, o);

                    foreach (var p in products1)
                    {
                        Console.WriteLine($"{p.Name} {p.Price}");
                    }
                }

                Console.WriteLine("__________________________________________");
                Console.WriteLine();

                // __________________________________________________________________________________________________________

                Console.WriteLine("Цена товара до редактирования:");

                var product = productRepository.GetById(1);

                Console.WriteLine($"{product.Name} {product.Price}");

                product.Price = 77;
                uow.Save();

                var productAfterPriceReview = productRepository.GetById(1);
                Console.WriteLine("Цена товара после редактирования:");
                Console.WriteLine($"{productAfterPriceReview.Name} {productAfterPriceReview.Price}");

                Console.WriteLine("__________________________________________");
                Console.WriteLine();

                // __________________________________________________________________________________________________________

                var products2 = productRepository.GetProductsNames();

                Console.WriteLine("Товары в наличии:");

                foreach (var p in products2)
                {
                    Console.WriteLine(p);
                }

                var removedProduct = productRepository.GetProductByName("Milk");

                using (var dbTransaction = productRepository.BeginTransaction())
                {
                    try
                    {
                        productRepository.Delete(removedProduct);
                        uow.Save();
                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                    }

                }

                var productsAfterSale = productRepository.GetProductsNames();

                Console.WriteLine("Товары в наличии после распродажи:");

                foreach (var p in productsAfterSale)
                {
                    Console.WriteLine(p);
                }

                Console.WriteLine("__________________________________________");
                Console.WriteLine();

                // __________________________________________________________________________________________________________

                var productId = productRepository.GetMostPurchasedProductId();

                Console.WriteLine("Самый часто покупаемый товар:");

                var productName = productRepository.GetById(productId).Name;

                Console.WriteLine(productName);

                Console.WriteLine("__________________________________________");
                Console.WriteLine();

                // __________________________________________________________________________________________________________

                var customers = customerRepository.GetCustomersWithOrders();

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

                Console.WriteLine("__________________________________________");
                Console.WriteLine();                

                // __________________________________________________________________________________________________________

                var categories = categoryRepository.GetCategoryWithProductsAndOrders();

                foreach (var c in categories)
                {
                    Console.WriteLine($"Категория: {c.Name}");

                    var products = c.ProductCategories;
                    var productsCount = products
                        .Select(p => p.Product.ProductOrders)
                        .Select(o => o.Count)
                        .Sum();

                    Console.WriteLine($"Продуктов куплено: {productsCount}");
                    Console.WriteLine();
                }
            }
        }
    }
}
