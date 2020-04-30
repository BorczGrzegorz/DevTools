using DevTools.Infrastructure.Models;
using Extensions.Binders;
using System;

namespace DevTools.Binders
{
    public class AddressIdBinder : IdBinder<AddressId>
    {
        protected override AddressId Parse(Guid id) => new AddressId(id);
    }
}
