using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopUnitOfWork.Model;
using ShopUnitOfWork.RepositoryInterfaces;

namespace ShopUnitOfWork.RepositoryClasses
{
    class CategoryRepository : BaseEfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext db) : base(db)
        {

        }

        public List<Category> GetCategoryWithProductsAndOrders()
        {
            return dbSet.Include(c => c.ProductCategories).ThenInclude(pc => pc.Product).ThenInclude(p => p.ProductOrders).ToList();
        }
    }
}
