using DevTools.Application.Abstract;
using DevTools.Application.Models.Dto;
using DevTools.JiraApi.Abstract;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.JiraApi
{
    public class UserQuery : IUserQuery
    {
        private readonly IJiraWebClient _client;

        public UserQuery(IJiraWebClient jiraWebClient)
        {
            _client = jiraWebClient ?? throw new ArgumentNullException(nameof(jiraWebClient));
        }

        public async Task<UserDto> GetUser()
        {
            string userName = await _client.GetUserName();
            JiraUserDto jiraUser = await _client.GetUser(userName);
            return jiraUser.ToUserDto();
        }

        public async Task<UserDto[]> GetUsers(string[] userName)
        {
            if (userName == null || !userName.Any())
            {
                return new UserDto[0];
            }
            JiraUserDto[] jiraUsers = await Task.WhenAll(userName.Select(x => _client.GetUser(x)));
            return jiraUsers.ToUsersDto();
        }
    }
}
