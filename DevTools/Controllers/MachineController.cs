using DevTools.Application.Abstract;
using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;
using Extensions.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Controllers
{
    [ApiVersionNeutral]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMachineQuery _machineQuery;

        public MachineController(IProductService productService,
                                 IMachineQuery machineQuery)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _machineQuery = machineQuery ?? throw new ArgumentNullException(nameof(machineQuery));
        }

        [HttpPut("/machines/product/{productId}")]
        public ActionResult<MachineDto[]> AddMachines([FromRoute]ProductId productId, [FromBody] NewMachineDto[] machines)
        {
            var machinesDto = _productService.AddMachines(productId, machines);
            return machinesDto;
        }

        [HttpGet("/machines/product/{productId}")]
        public ActionResult<MachineDto[]> GetMachines([FromRoute]ProductId productId)
            => _machineQuery.Get(productId);

        [HttpDelete("/machines/product/{productId}/machine/{machineId}")]
        public ActionResult<MachineDto> DeleteMachine([FromRoute]ProductId productId, [FromRoute]MachineId machineId)
        {
            return _productService. RemoveMachine(productId, machineId);
        }
    }
}
