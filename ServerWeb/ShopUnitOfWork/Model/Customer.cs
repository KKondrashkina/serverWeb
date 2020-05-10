using System.Collections.Generic;

namespace ShopUnitOfWork.Model
{
    public class Customer
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}