using DevTools.Infrastructure.Models;
using System;

namespace DevTools.DataAccess.Serializers
{
    internal class ProductIdSerializer : IdSerializer<ProductId>
    {
        protected override ProductId Convert(Guid? value) => (ProductId)value;

        protected override Guid? Convert(ProductId model) => (Guid?)model;
    }
}
