using Microsoft.EntityFrameworkCore;
using ShopUnitOfWork.Model;
using ShopUnitOfWork.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ShopUnitOfWork.RepositoryClasses
{
    class CustomerRepository : BaseEfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext db) : base(db)
        {

        }

        public List<Customer> GetCustomersWithOrders()
        {
            return DbSet
                .Include(c => c.Orders)
                .ThenInclude(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .ToList();
        }        
    }
}
