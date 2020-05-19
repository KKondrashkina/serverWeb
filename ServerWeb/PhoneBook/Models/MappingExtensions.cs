namespace PhoneBook.Models
{
    public static class MappingExtensions
    {
        public static Contact ToModel(this ContactDto contactDto)
        {
            return new Contact
            {
                Id = contactDto.Id,
                Name = contactDto.Name,
                LastName = contactDto.LastName,
                PhoneNumber = contactDto.PhoneNumber
            };
        }

        public static ContactDto ToDto(this Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber
            };
        }
    }
}