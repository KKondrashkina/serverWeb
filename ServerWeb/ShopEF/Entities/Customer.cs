using System.Collections.Generic;

namespace ShopEF.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } =
            new List<Order>();
    }
}
