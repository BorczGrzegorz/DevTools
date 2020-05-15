using DevTools.Application.Abstract;
using DevTools.Application.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Controllers
{
    [ApiVersionNeutral]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserQuery _userQuery;

        public UserController(IUserQuery userQuery)
        {
            _userQuery = userQuery ?? throw new ArgumentNullException(nameof(userQuery));
        }

        [HttpGet("/users/self")]
        public async Task<ActionResult<UserDto>> GetUser() => await _userQuery.GetUser();

        [HttpGet("/users")]
        public async Task<ActionResult<UserDto[]>> GetUsers([FromQuery] string[] userName)
            => await _userQuery.GetUsers(userName);
    }
}
