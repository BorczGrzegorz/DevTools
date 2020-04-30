using DevTools.Infrastructure.Models;
using System;

namespace DevTools.Converters
{
    public class AddressIdConverter : IdConverter<AddressId>
    {
        protected override AddressId Create(Guid value) => new AddressId(value);
    }
}
