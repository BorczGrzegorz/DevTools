using DevTools.Application.Models.SearchParams;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.Mock
{
    public class IssuesResultDtoCollectionBuilder : CollectionBuilder<IssuesResultDtoCollectionBuilder, JiraIssueDto>
    {
        private static Random _rand = new Random();
        private IssuesResultDtoCollectionBuilder()
        { }

        public static IssuesResultDtoCollectionBuilder Empty()
            => new IssuesResultDtoCollectionBuilder();

        public new IssuesResultDto Build() => new IssuesResultDto
        {
            Issues = Models.ToArray()
        };

        public IssuesResultDtoCollectionBuilder AddMany(int fromId, int toId)
            => For(toId - fromId, i => Add(fromId + i));

        public IssuesResultDtoCollectionBuilder Add(int id)
            => Add(id, $"key {id}");

        public IssuesResultDtoCollectionBuilder Add(int id, string key)
            => Add(id, key, $"{key} summary");

        public IssuesResultDtoCollectionBuilder Add(int id, string key, string summary)
        {
            Add(new JiraIssueDto
            {
                Id = id,
                Key = key,
                Fields = new IssueFields
                {
                    Summary = summary,
                    Status = new StatusCategoryDto
                    {
                        Id = GetRandomState()
                    }
                }
            });
            return this;
        }

        private static IssueState GetRandomState()
        {
            Array values = Enum.GetValues(typeof(IssueState));
            return (IssueState)values.GetValue(_rand.Next(values.Length));
        }
    }
}
