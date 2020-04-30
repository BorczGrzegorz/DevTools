using System;
using System.Collections.Generic;

namespace Extensions.Infrastructure.Models
{
    public class MachineId
    {
        private readonly Guid _id;
        public static readonly MachineId Empty = new MachineId(Guid.Empty);

        public MachineId(Guid id)
        {
            _id = id;
        }

        public static MachineId NewId() => new MachineId(Guid.NewGuid());

        public static explicit operator MachineId(Guid id)
        {
            return new MachineId(id);
        }

        public static explicit operator Guid(MachineId machineId)
        {
            return machineId == null ? Guid.Empty : machineId._id;
        }

        public static bool operator ==(MachineId x, MachineId y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x?._id == y?._id;
        }

        public static bool operator !=(MachineId x, MachineId y)
        {
            return !(x == y);
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        public override bool Equals(object obj)
        {
            var id = (MachineId)obj;
            return id != null &&
                   _id.Equals(id._id);
        }

        public override int GetHashCode() => EqualityComparer<Guid>.Default.GetHashCode(_id);
    }
}
