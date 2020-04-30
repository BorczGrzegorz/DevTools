using DevTools.Application.Models.SearchParams;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.Mock
{
    public class JiraWorkLogDtoBuilder
    {
        private static Random _rand = new Random();
        private JiraWorkLogDto _workLog = new JiraWorkLogDto();
        private JiraWorkLogDtoBuilder()
        { }

        public static JiraWorkLogDtoBuilder Empty() => new JiraWorkLogDtoBuilder();
        public static JiraWorkLogDtoBuilder Filled()
            => Empty()
               .Id(_rand.Next(0, 10000000))
               .IssueId(_rand.Next(0, 10000000))
               .TimeSpentSeconds(_rand.Next(60, 60 * 60 * 8))
               .Created(DateTime.UtcNow.AddDays((int)_rand.Next(0, 10)))
               .Author(JiraUserDtoBuilder.FromDisplayName(_rand.Next(10000).ToString()).Build());

        public JiraWorkLogDto Build() => new JiraWorkLogDto(_workLog);

        public JiraWorkLogDtoBuilder Id(int id) => Set(w => w.Id = id);
        public JiraWorkLogDtoBuilder IssueId(int id) => Set(w => w.IssueId = id);
        public JiraWorkLogDtoBuilder TimeSpentSeconds(int timeSpent) => Set(w => w.TimeSpentSeconds = timeSpent);
        public JiraWorkLogDtoBuilder Created(DateTime created) => Set(w => w.Created = created);
        public JiraWorkLogDtoBuilder Author(Func<JiraUserDtoBuilder, JiraUserDtoBuilder> action)
            => Author(action(JiraUserDtoBuilder.Empty()).Build());
        public JiraWorkLogDtoBuilder Author(JiraUserDto author) => Set(w => w.Author = author);

        private JiraWorkLogDtoBuilder Set(Action<JiraWorkLogDto> action)
        {
            action(_workLog);
            return this;
        }
    }
}
