using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopEF
{
    class Product
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public int Price { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } =
            new List<ProductOrder>();

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } =
            new List<ProductCategory>();
    }
}
