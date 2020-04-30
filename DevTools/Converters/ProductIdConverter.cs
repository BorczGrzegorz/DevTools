using DevTools.Infrastructure.Models;
using System;

namespace DevTools.Converters
{
    public class ProductIdConverter : IdConverter<ProductId>
    {
        protected override ProductId Create(Guid value) => new ProductId(value);
    }
}
