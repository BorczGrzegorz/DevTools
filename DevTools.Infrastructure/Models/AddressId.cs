using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Infrastructure.Models
{
    public class AddressId
    {
        private readonly Guid _id;
        public static readonly AddressId Empty = new AddressId(Guid.Empty);

        public AddressId(Guid id)
        {
            _id = id;
        }

        public static AddressId NewId() => new AddressId(Guid.NewGuid());

        public static explicit operator AddressId(Guid id)
        {
            return new AddressId(id);
        }

        public static explicit operator Guid(AddressId machineId)
        {
            return machineId == null ? Guid.Empty : machineId._id;
        }

        public static bool operator ==(AddressId x, AddressId y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x?._id == y?._id;
        }

        public static bool operator !=(AddressId x, AddressId y)
        {
            return !(x == y);
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        public override bool Equals(object obj)
        {
            var id = (AddressId)obj;
            return id != null &&
                   _id.Equals(id._id);
        }

        public override int GetHashCode() => EqualityComparer<Guid>.Default.GetHashCode(_id);
    }
}
