using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopUnitOfWork.Model
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}