using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopMigration.DataAccess.Model
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string BirthDay { get; set; }

        public virtual ICollection<Order> Orders { get; set; } =
            new List<Order>();
    }
}