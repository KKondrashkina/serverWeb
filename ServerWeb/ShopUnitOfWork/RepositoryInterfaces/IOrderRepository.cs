using System.Collections.Generic;
using ShopUnitOfWork.Model;

namespace ShopUnitOfWork.RepositoryInterfaces
{
    interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetOrdersWithProducts();

        List<Product> GetOrderProductsMoreExpensiveThan(double price, Order o);

        List<Order> GetOrdersWithProductsAndCustomers();
    }
}
