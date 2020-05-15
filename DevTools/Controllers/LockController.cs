using Extensions.Infrastructure.Models;
using DevTools.Application;
using DevTools.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTools.Controllers
{
    [ApiVersionNeutral]
    [ApiController]
    public class LockController : Controller
    {
        private IMachineLockService _machineLockService;

        public LockController(IMachineLockService machineLockService)
        {
            _machineLockService = machineLockService;
        }

        /// <summary>
        /// Zalokowanie maszyny dla użytkownika
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPut("/lock/machineName/{machineId}/userName/{userName}")]
        public ActionResult<MachineState> Lock([FromRoute]MachineId machineId, [FromRoute]string userName)
        {
            try
            {
                return Json(_machineLockService.Lock(machineId, userName));
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Zwolnienie maszyny przez użytkownika
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPut("/release/machineName/{machineId}/userName/{userName}")]
        public IActionResult Release([FromRoute]MachineId machineId, [FromRoute]string userName)
        {
            try
            {
                _machineLockService.Release(machineId, userName);
                return Ok();
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Pobranie stanu aplikacji
        /// </summary>
        /// <returns></returns>
        [HttpGet("/machines/states")]
        [Produces(typeof(List<MachineState>))]
        public ActionResult<List<MachineState>> GetAll()
        {
            return _machineLockService.GetAllStates();
        }
    }
}
