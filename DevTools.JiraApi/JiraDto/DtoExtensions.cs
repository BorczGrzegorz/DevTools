using DevTools.Application.Models.Dto;
using DevTools.Application.Models.Dto.WorkLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTools.JiraApi.JiraDto
{
    internal static class DtoExtensions
    {
        public static UserDto[] ToUsersDto(this JiraUserDto[] jiraUsers)
            => jiraUsers.Select(x => x.ToUserDto()).ToArray();

        public static UserDto ToUserDto(this JiraUserDto jiraUserDto)
        {
            return new UserDto
            {
                Name = jiraUserDto.Name,
                Key = jiraUserDto.Key,
                DisplayName = jiraUserDto.DisplayName,
                EmailAddress = jiraUserDto.EmailAddress,
                AvatarsUrls = new Application.Models.Dto.Users.Avatars
                {
                    Url48x48 = jiraUserDto.AvatarUrls["48x48"],
                    Url32x32 = jiraUserDto.AvatarUrls["32x32"],
                    Url24x24 = jiraUserDto.AvatarUrls["24x24"],
                    Url16x16 = jiraUserDto.AvatarUrls["16x16"]
                }
            };
        }
    }
}
