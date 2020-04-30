using DevTools.Infrastructure.Models;

namespace DevTools.Application.Models.Dto
{
    public class AddressDto
    {
        public AddressId Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsSingleUrl { get; set; }

        public AddressDto()
        {

        }

        public AddressDto(Address address)
        {
            Id = address.Id;
            Name = address.Name;
            Path = address.Path;
            IsSingleUrl = address.IsSingleUrl;
        }
    }
}
