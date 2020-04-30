using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;
using System;

namespace DevTools.Application.Models
{
    public class Address
    {
        public AddressId Id { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public bool IsSingleUrl { get; private set; }

        public Address(string name, string path, bool isSingleUrl)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Path = path ?? throw new ArgumentNullException(nameof(path));
            IsSingleUrl = isSingleUrl;
            Id = AddressId.NewId();
        }

        public Address(NewAddressDto addressDto) 
            : this(addressDto.Name,
                   addressDto.Path, 
                   addressDto.IsSingleUrl)
        {}
    }
}
