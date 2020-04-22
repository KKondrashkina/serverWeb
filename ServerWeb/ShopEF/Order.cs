using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopEF
{
    class Order
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Date { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } =
            new List<ProductOrder>();
    }
}
