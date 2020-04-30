using Microsoft.EntityFrameworkCore;
using ShopUnitOfWork.Model;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork.RepositoryClasses
{
    class CustomerRepository : BaseEfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext db) : base(db)
        {

        }
    }
}
