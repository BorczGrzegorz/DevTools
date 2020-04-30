using DevTools.Application.Abstract;
using DevTools.Application.Models;
using DevTools.Application.Models.Dto;
using DevTools.Application.Models.Dto.Issues;
using DevTools.Application.Models.Dto.WorkLog;
using DevTools.Application.Models.SearchParams;
using DevTools.Application.Models.SearchParams.Abstract;
using DevTools.JiraApi.Abstract;
using DevTools.JiraApi.Exceptions;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.JiraApi
{
    public class WorklogQuery : IWorkLogQuery
    {
        private readonly IJiraWebClient _client;
        private readonly string _boardId;

        public WorklogQuery(IJiraWebClient jiraWebClient, string boardId)
        {
            _client = jiraWebClient ?? throw new ArgumentNullException(nameof(jiraWebClient));
            _boardId = boardId;
        }

        public async Task<UsersDatesSummary> GetUserDatesSummary(SearchParamsDto searchParams)
        {
            IEnumerable<SprintDto> sprints = await GetSprints(searchParams);
            List<JiraIssueDto> issues = await _client.GetIssues(searchParams, sprints);
            List<JiraWorkLogDto> workloads = (await _client.GetWorkLoads(issues)).ToList();

            UsersDatesSummary dto = workloads.FilterByUsers(searchParams)
                                             .FilterByStartFromAndSrpintDates(searchParams, sprints)
                                             .GroupByAuthor()
                                             .ToDateIssueSummaryDictionary(issues);

            return dto;
        }

        public async Task<List<IssueDto>> GetIssues(IssueSearchParamsDto searchParams)
        {
            IEnumerable<SprintDto> sprints = await GetSprints(searchParams);
            List<JiraIssueDto> issues = await _client.GetIssues(searchParams, sprints);
            return issues.Select(x => new IssueDto()
            {
                Id = x.Id,
                Key = x.Key,
                Summary = x.Fields.Summary
            }).ToList();
        }

        public async Task<UsersIssueDto> GetIssues(SearchParamsDto searchParams)
        {
            IEnumerable<SprintDto> sprints = await GetSprints(searchParams);
            List<JiraIssueDto> issues = await _client.GetIssues(searchParams, sprints);
            IEnumerable<JiraWorkLogDto> workloads = await _client.GetWorkLoads(issues);

            UsersIssueDto dto = workloads.FilterByUsers(searchParams)
                                         .FilterByStartFromAndSrpintDates(searchParams, sprints)
                                         .GroupByAuthor()
                                         .ToIssueWorklogDictionary(issues);

            return dto;
        }

        public async Task<UsersWorkLogDto> GetWorkLogs(SearchParamsDto searchParams)
        {
            IEnumerable<SprintDto> sprints = await GetSprints(searchParams);
            var issues = await _client.GetIssues(searchParams, sprints);
            IEnumerable<JiraWorkLogDto> workloads = await _client.GetWorkLoads(issues);

            UsersWorkLogDto dto = workloads.FilterByUsers(searchParams)
                                           .FilterByStartFromAndSrpintDates(searchParams, sprints)
                                           .GroupByAuthor()
                                           .ToUsersWorkLogDto();

            return dto;
        }

        private async Task<IEnumerable<SprintDto>> GetSprints<T>(T searchParams)
            where T : IDateFilter, ISprintFilter
        {
            var descriptions = await _client.GetSprintsDescriptions(_boardId);
            IEnumerable<SprintDescriptionDto> selected = descriptions
                                                        .Sprints
                                                        .FilterState(searchParams, (sprint, s) => sprint.State == s);

            DateTime startFrom = searchParams.GetDefaultStartFrom();
            if (searchParams.StartFrom.HasValue)
            {
                startFrom = searchParams.StartFrom.Value;
            }

            return await selected.FilterByDate(startFrom, x => _client.GetSprintDetails(_boardId, x.Id));
        }
    }
}
