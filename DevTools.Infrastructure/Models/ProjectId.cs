using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Infrastructure.Models
{
    public class ProjectId
    {
        private readonly Guid _id;
        public static readonly ProjectId Empty = new ProjectId(Guid.Empty);

        public ProjectId(Guid id)
        {
            _id = id;
        }

        public static ProjectId NewId() => new ProjectId(Guid.NewGuid());

        public static explicit operator ProjectId(Guid id)
        {
            return new ProjectId(id);
        }

        public static explicit operator Guid(ProjectId machineId)
        {
            return machineId == null ? Guid.Empty : machineId._id;
        }

        public static bool operator ==(ProjectId x, ProjectId y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x?._id == y?._id;
        }

        public static bool operator !=(ProjectId x, ProjectId y)
        {
            return !(x == y);
        }

        public override string ToString()
        {
            return _id.ToString();
        }

        public override bool Equals(object obj)
        {
            var id = (ProjectId)obj;
            return id != null &&
                   _id.Equals(id._id);
        }

        public override int GetHashCode() => EqualityComparer<Guid>.Default.GetHashCode(_id);
    }
}
