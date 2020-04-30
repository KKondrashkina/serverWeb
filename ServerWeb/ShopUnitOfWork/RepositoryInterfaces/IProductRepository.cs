using System.Collections.Generic;
using ShopUnitOfWork.Model;

namespace ShopUnitOfWork.RepositoryInterfaces
{
    interface IProductRepository : IRepository<Product>
    {
        void AddCategoryToProduct(Product p, Category c);

        void AddProductToOrder(Product p, Order o);

        List<string> GetProductsNames();

        Product GetProductByName(string name);

        int GetMostPurchasedProductId();
    }
}
