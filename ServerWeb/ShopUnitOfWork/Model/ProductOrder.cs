﻿namespace ShopUnitOfWork.Model
{
    public class ProductOrder
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int ProductCount { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}