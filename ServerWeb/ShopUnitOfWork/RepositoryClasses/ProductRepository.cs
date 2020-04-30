using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopUnitOfWork.Model;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork.RepositoryClasses
{
    class ProductRepository : BaseEfRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {

        }

        public void AddCategoryToProduct(Product p, Category c)
        {
            p.ProductCategories.Add(new ProductCategory { ProductId = p.Id, CategoryId = c.Id });
        }

        public void AddProductToOrder(Product p, Order o)
        {
            p.ProductOrders.Add(new ProductOrder { ProductId = p.Id, OrderId = o.Id });
        }

        public List<string> GetProductsNames()
        {
            return dbSet.Select(p => p.Name).ToList();
        }

        public Product GetProductByName(string name)
        {
            return dbSet.FirstOrDefault(p => p.Name == name);
        }

        public int GetMostPurchasedProductId()
        {
            var products3 = dbSet.Include(p => p.ProductOrders).ToList();
            var productsIds = new List<int>();

            foreach (var r in products3)
            {
                productsIds.AddRange(r.ProductOrders.Select(p => p.ProductId));
            }

            return productsIds.GroupBy(p => p).OrderByDescending(o => o.Count()).FirstOrDefault().Key;
        }
    }
}
