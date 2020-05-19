using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class ContactDto
    {
        public int Id { get; set; }

        [MaxLength(25)]
        [Required]
        [RegularExpression("^[A-Za-zА-Яа-яЁё]+$", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set; }

        [MaxLength(25)]
        [Required]
        [RegularExpression("^[A-Za-zА-Яа-яЁё]+$", ErrorMessage = "Only letters are allowed")]
        public string LastName { get; set; }

        [MaxLength(15)]
        [Required]
        [RegularExpression(@"\+7-[0-9]{3}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Please enter the phone in the format +7-XXX-XXX-XXXX")]
        public string PhoneNumber { get; set; }
    }
}
