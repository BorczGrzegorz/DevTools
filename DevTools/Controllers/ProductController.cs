using DevTools.Application.Abstract;
using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DevTools.Controllers
{
    [ApiVersionNeutral]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductQuery _productQuery;

        public ProductController(IProductService productService, 
                                 IProductQuery productQuery)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _productQuery = productQuery ?? throw new ArgumentNullException(nameof(productQuery));
        }

        [HttpPost("name/{productName}")]
        public ActionResult<ProductDto> Add([FromRoute]string productName) => _productService.Add(productName);

        [HttpGet]
        public ActionResult<List<ProductDto>> GetAllProducts() 
            => _productQuery.GetAll();

        [HttpGet("{productId}")]
        public ActionResult<ProductDto> GetProductById([FromRoute] ProductId productId)=> _productQuery.Get(productId);
    }
}
