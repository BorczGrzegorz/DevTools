using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Infrastructure.Models
{
    public class ProductId
    {
        private readonly Guid _id;
        public static readonly ProductId Empty = new ProductId(Guid.Empty);

        public ProductId(Guid id)
        {
            _id = id;
        }

        public static ProductId NewId() => new ProductId(Guid.NewGuid());

        public static explicit operator ProductId(Guid id)
        {
            return new ProductId(id);
        }

        public static explicit operator Guid(ProductId machineId)
        {
            return machineId == null ? Guid.Empty : machineId._id;
        }

        public static bool operator ==(ProductId x, ProductId y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x?._id == y?._id;
        }

        public static bool operator !=(ProductId x, ProductId y)
        {
            return !(x == y);
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        public override bool Equals(object obj)
        {
            var id = (ProductId)obj;
            return id != null &&
                   _id.Equals(id._id);
        }

        public override int GetHashCode() => EqualityComparer<Guid>.Default.GetHashCode(_id);
    }
}
