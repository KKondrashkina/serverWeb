using System;
using System.Collections.Generic;

namespace ShopUnitOfWork.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}