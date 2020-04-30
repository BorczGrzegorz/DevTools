using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.JiraApi.JiraDto
{
    public class JiraUserDto
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<string, string> AvatarUrls { get; set; }

        public JiraUserDto()
        {

        }

        public JiraUserDto(JiraUserDto user)
        {
            Name = user.Name;
            Key = user.Key;
            EmailAddress = user.EmailAddress;
            DisplayName = user.DisplayName;
            AvatarUrls = user.AvatarUrls.ToDictionary(x => x.Key, y=> y.Value);
        }
    }
}
