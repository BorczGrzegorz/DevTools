using DevTools.Application.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Application.Abstract
{
    public interface IUserQuery
    {
        Task<UserDto> GetUser();
        Task<UserDto[]> GetUsers(string[] userName);
    }
}
