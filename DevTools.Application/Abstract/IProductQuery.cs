using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Application.Abstract
{
    public interface IProductQuery
    {
        List<ProductDto> GetAll();
        ProductDto Get(ProductId id);
    }
}
