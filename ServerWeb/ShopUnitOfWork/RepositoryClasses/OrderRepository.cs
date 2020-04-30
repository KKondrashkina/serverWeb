using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopUnitOfWork.Model;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork.RepositoryClasses
{
    class OrderRepository : BaseEfRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext db) : base(db)
        {

        }

        public List<Order> GetOrdersWithProducts()
        {
            return dbSet.Include(o => o.ProductOrders).ThenInclude(po => po.Product).ToList();
        }

        public List<Product> GetOrderProductsMoreExpensiveThan(double price, Order o)
        {
            return o.ProductOrders.Select(po => po.Product).Where(p => p.Price > price).ToList();
        }

        public List<Order> GetOrdersWithProductsAndCustomers()
        {
            return dbSet.Include(o => o.ProductOrders).ThenInclude(po => po.Product).Include(c => c.Customer).ToList();
        }
    }
}
