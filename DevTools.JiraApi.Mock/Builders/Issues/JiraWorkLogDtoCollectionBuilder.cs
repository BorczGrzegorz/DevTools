using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.Mock
{
    public class JiraWorkLogDtoCollectionBuilder : CollectionBuilder<JiraWorkLogDtoCollectionBuilder, JiraWorkLogDto>
    {
        private static Random _rand = new Random();
        private JiraWorkLogDtoCollectionBuilder()
        { }

        public static JiraWorkLogDtoCollectionBuilder Empty() => new JiraWorkLogDtoCollectionBuilder();

        public JiraWorkLogDtoCollectionBuilder AddManyForAuthor(JiraUserDto author,
                                                                int count,
                                                                int issueId,
                                                                DateTime start,
                                                                DateTime end)
            => For(count, () => Add(author, issueId, GetRandomDateTime(start, end)));

        public JiraWorkLogDtoCollectionBuilder Add(JiraUserDto author, int issueId, DateTime created)
            => Add(JiraWorkLogDtoBuilder.Filled()
                                        .Author(author)
                                        .IssueId(issueId)
                                        .Created(created)
                                        .Build());

        private static DateTime GetRandomDateTime(DateTime start, DateTime end)
        {
            TimeSpan range = end - start;
            return start.AddSeconds(_rand.Next(0, (int)range.TotalSeconds));
        }
    }
}
