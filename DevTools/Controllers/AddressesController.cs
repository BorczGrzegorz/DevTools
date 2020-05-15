using DevTools.Application.Abstract;
using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Controllers
{
    [ApiVersionNeutral]
    [ApiController]
    [Route("addresses")]
    public class AddressesController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAddressesQuery _addressesQuery;

        public AddressesController(IProductService productService,
                                   IAddressesQuery addressesQuery)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _addressesQuery = addressesQuery ?? throw new ArgumentNullException(nameof(addressesQuery));
        }

        [HttpPut("project/{projectId}")]
        public ActionResult<AddressDto[]> AddAddresses([FromRoute]ProjectId projectId, [FromBody] NewAddressDto[] addresses)
            => _productService.AddAddresses(projectId, addresses);

        [HttpDelete("project/{projectId}/address/{addressId}")]
        public ActionResult<AddressDto> DeleteAddresses([FromRoute]ProjectId projectId, [FromRoute] AddressId addressId)
            => _productService.RemoveAddress(projectId, addressId);

        [HttpGet("project/{projectId}")]
        public ActionResult<AddressDto[]> GetAddresses([FromRoute] ProjectId projectId)
            => _addressesQuery.GetAddresses(projectId);
    }
}
