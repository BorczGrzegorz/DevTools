using DevTools.Application.Models;
using DevTools.Application.Models.SearchParams;
using DevTools.JiraApi.Abstract;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.JiraApi.Mock
{
    public class JiraMockWebClient : IJiraWebClient
    {
        private const string CURRENT_USER_DISPLAY_NAME = "John Smith";
        private static List<JiraUserDto> _users;
        private static List<SprintDto> _sprints;
        private static Dictionary<int, JiraIssueDto[]> _sprintIssueDictionary = new Dictionary<int, JiraIssueDto[]>();
        private static List<JiraWorkLogDto> _workLogs = new List<JiraWorkLogDto>();

        static JiraMockWebClient()
        {
            _users = JiraUserDtoCollectionBuilder
                     .FromNames(CURRENT_USER_DISPLAY_NAME, 
                                 "John Doe", 
                                 "Jack Nicolson", 
                                 "Catherine Bell",
                                 "Johny Depp",
                                 "Anthony Hopkins",
                                 "Edward Norton",
                                 "Joaquin Phoenix",
                                 "Scarlett Johansson",
                                 "Eva Green",
                                 "Leonardo DiCaprio",
                                 "Jennifer Aniston")
                     .Build();

            _sprints = SprintDtoCollectionBuilder
                       .Empty()
                       .AddMany(0, 4, TimeSpan.FromDays(7), SprintState.CLOSED)
                       //.Add(4, TimeSpan.FromDays(7), SprintState.ACTIVE)
                       .AddActive(4, new DateTime(2020,2,20), new DateTime(2020,3,10))
                       .Build();

            for (int i = 0; i < _sprints.Count; i++)
            {
                var issues = IssuesResultDtoCollectionBuilder.Empty().AddMany(i * 2, (i * 2) + 2).Build();

                foreach (JiraIssueDto issue in issues.Issues)
                {
                    foreach (JiraUserDto user in _users)
                    {
                        var workLogs = JiraWorkLogDtoCollectionBuilder
                                    .Empty()
                                    .AddManyForAuthor(user, 
                                                      20, 
                                                      issue.Id, 
                                                      _sprints[i].StartDate.AddDays(-10),
                                                      _sprints[i].EndDate)
                                    .Build();

                        _workLogs.AddRange(workLogs);
                    }
                }

                _sprintIssueDictionary.Add(i, issues.Issues);
            }
        }

        public Task<List<JiraIssueDto>> GetIssues(params int[] sprints)
        {
            return Task.FromResult(_sprintIssueDictionary
                                    .Where(x => sprints.Contains(x.Key))
                                    .SelectMany(x => x.Value)
                                    .ToList()
            );
        }

        public async Task<List<JiraIssueDto>> GetIssues(IssueState? issueState, params int[] sprints)
        {
            var issues = await GetIssues(sprints);
            if (issueState.HasValue)
            {
                issues = issues.Where(x => x.Fields.Status.Id == issueState.Value).ToList();
            }

            return issues;
        }

        public Task<List<JiraIssueDto>> GetIssues(string issueAssignee, params int[] sprints)
        {
            return GetIssues(sprints);
        }

        public Task<List<JiraIssueDto>> GetIssues(IssueState? issueState, string issueAssignee, params int[] sprints)
        {
            return GetIssues(issueState, sprints);
        }

        public async Task<List<JiraIssueDto>> GetIssues(List<IssueState> issueState, 
                                                        List<IssueState> notIssueState, 
                                                        string issueAssignee, 
                                                        params int[] sprints)
        {
            return (await GetIssues(issueAssignee, sprints))
                   .WhereIf(issueState != null, x => issueState.Contains(x.Fields.Status.Id))
                   .WhereIf(notIssueState != null, x => !notIssueState.Contains(x.Fields.Status.Id))
                   .ToList();
        }

        public Task<SprintDto> GetSprintDetails(string boardId, int sprintId)
        {
            return Task.FromResult(_sprints.SingleOrDefault(x => x.Id == sprintId));
        }

        public Task<SprintDescriptionResultsDto> GetSprintsDescriptions(string boardId)
        {
            return Task.FromResult(SprintDescriptionResultsDtoBuilder
                                   .FromSprintDtos(_sprints.OrderBy(x => x.StartDate).ToList())
                                   .Build());
        }

        public Task<JiraUserDto> GetUser(string name)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Key == name));
        }

        public Task<string> GetUserName()
        {
            return Task.FromResult(_users.Single(x => x.DisplayName == CURRENT_USER_DISPLAY_NAME).Key);
        }

        public Task<List<JiraWorkLogDto>> GetWorkLogs(int issueId)
        {
            return Task.FromResult(_workLogs.Where(x => x.IssueId == issueId).ToList());
        }

        public Task LogWork(string issueId, int minutes)
        {
            return Task.CompletedTask;
        }
    }
}
