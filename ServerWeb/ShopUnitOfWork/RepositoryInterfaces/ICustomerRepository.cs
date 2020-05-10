using ShopUnitOfWork.Model;
using System.Collections.Generic;

namespace ShopUnitOfWork.RepositoryInterfaces
{
    interface ICustomerRepository : IRepository<Customer>
    {
        List<Customer> GetCustomersWithOrders();
    }
}
