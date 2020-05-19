using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Models
{
    public class PhoneBookContext: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public PhoneBookContext(DbContextOptions<PhoneBookContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
