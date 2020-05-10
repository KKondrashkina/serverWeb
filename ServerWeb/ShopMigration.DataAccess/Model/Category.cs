using System.Collections.Generic;

namespace ShopMigration.DataAccess.Model
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } =
            new List<ProductCategory>();
    }
}