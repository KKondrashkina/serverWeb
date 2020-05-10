using System.Collections.Generic;

namespace ShopUnitOfWork.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}