using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Exceptions;
using DevTools.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.Application.Models
{
    public class Project
    {
        private List<Address> _addresses = new List<Address>();
        public ProjectId Id { get; private set; }
        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
        public string Name { get; private set; }

        public Project(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = ProjectId.NewId();
        }

        internal Address DeleteAddress(AddressId addressId)
        {
            Address addressToRemove = _addresses.SingleOrDefault(x => x.Id == addressId);
            if (addressToRemove == null)
            {
                throw new EntityNotEixtException($"Address {addressId} does not exist!");
            }

            _addresses.Remove(addressToRemove);
            return addressToRemove;
        }

        internal Address AddAddress(NewAddressDto addressDto)
        {
            Address address = new Address(addressDto);
            _addresses.Add(address);
            return address;
        }

        internal Address[] AddAddresses(NewAddressDto[] addresses)
        {
            foreach (NewAddressDto address in addresses)
            {
                AddAddress(address);
            }

            return Addresses.Skip(Addresses.Count - addresses.Length).ToArray();
        }
    }
}
