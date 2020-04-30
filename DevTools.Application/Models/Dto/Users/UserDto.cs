using DevTools.Application.Models.Dto.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Application.Models.Dto
{
    public class UserDto
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Avatars AvatarsUrls { get; set; }
        public string DisplayName { get; set; }
    }
}
