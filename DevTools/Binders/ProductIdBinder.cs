using DevTools.Infrastructure.Models;
using Extensions.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Binders
{
    public class ProductIdBinder : IdBinder<ProductId>
    {
        protected override ProductId Parse(Guid id) => new ProductId(id);
    }
}
