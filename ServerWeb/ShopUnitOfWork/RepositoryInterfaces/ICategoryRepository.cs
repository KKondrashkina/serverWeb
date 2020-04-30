using System.Collections.Generic;
using ShopUnitOfWork.Model;

namespace ShopUnitOfWork.RepositoryInterfaces
{
    interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetCategoryWithProductsAndOrders();
    }
}
