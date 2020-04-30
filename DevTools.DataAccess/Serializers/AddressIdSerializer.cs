using DevTools.Infrastructure.Models;
using System;

namespace DevTools.DataAccess.Serializers
{
    internal class AddressIdSerializer : IdSerializer<AddressId>
    {
        protected override AddressId Convert(Guid? value) => (AddressId)value;

        protected override Guid? Convert(AddressId model) => (Guid?)model;
    }
}
