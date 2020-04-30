using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.Mock
{
    public class JiraUserDtoBuilder
    {
        private readonly JiraUserDto _user;

        private JiraUserDtoBuilder()
        {
            _user = new JiraUserDto();
            _user.AvatarUrls = new Dictionary<string, string>();
        }

        public JiraUserDto Build() => new JiraUserDto(_user);

        public static JiraUserDtoBuilder Empty() => new JiraUserDtoBuilder();
        public static JiraUserDtoBuilder FromDisplayName(string name) 
            => Empty()
               .SetName(name)
               .SetKey(name.ToLower().Replace(' ', '.'))
               .SetEmail($"{name.Replace(' ','.')}@gmail.com")
               .SetDisplayName(name)
               .AddAvatarUrl("48x48", "http://previews.123rf.com/images/inegvin/inegvin1701/inegvin170100077/69882112-user-sign-icon-person-symbol-human-avatar-.jpg")
               .AddAvatarUrl("32x32", "http://address")
               .AddAvatarUrl("24x24", "http://address")
               .AddAvatarUrl("16x16", "http://address");

        public JiraUserDtoBuilder SetName(string name) => Set(x => x.Name = name);
        public JiraUserDtoBuilder SetKey(string key) => Set(x => x.Key = key);
        public JiraUserDtoBuilder SetEmail(string email) => Set(x => x.EmailAddress = email);
        public JiraUserDtoBuilder SetDisplayName(string name) => Set(x => x.DisplayName = name);
        public JiraUserDtoBuilder AddAvatarUrl(string size, string address)
        {
            _user.AvatarUrls[size] = address;
            return this;
        }

        private JiraUserDtoBuilder Set(Action<JiraUserDto> action)
        {
            action(_user);
            return this;
        }
    }
}
