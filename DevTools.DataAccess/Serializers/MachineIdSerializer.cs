using Extensions.Infrastructure.Models;
using System;

namespace DevTools.DataAccess.Serializers
{
    internal class MachineIdSerializer: IdSerializer<MachineId>
    {
        protected override MachineId Convert(Guid? value) => (MachineId)value;

        protected override Guid? Convert(MachineId model) => (Guid?)model;
    }

}
