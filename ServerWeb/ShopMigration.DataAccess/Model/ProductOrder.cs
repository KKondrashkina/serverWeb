namespace ShopMigration.DataAccess.Model
{
    public class ProductOrder
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ProductCount { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}