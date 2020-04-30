using DevTools.Application.Models.SearchParams;
using DevTools.JiraApi.Abstract;
using DevTools.JiraApi.Builders;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevTools.JiraApi
{
    public class JiraWebClient : IJiraWebClient
    {
        private readonly HttpClient _httpClient;

        public JiraWebClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetUserName()
        {
            HttpResponseMessage message = await _httpClient.GetAsync("jira/secure/ViewProfile.jspa");
            string jspPage = await message.Content.ReadAsStringAsync();
            Regex pattern = new Regex("<meta name=\"ajs-remote-user\" content=\".*\">");
            Match match = pattern.Match(jspPage);
            pattern = new Regex(@"(\w+\.)+\w+");
            match = pattern.Match(match.Groups[0].Value);
            return match.Groups[0].Value;
        }

        public async Task<JiraUserDto> GetUser(string userName)
        {
            HttpResponseMessage message = await _httpClient.GetAsync($"jira/rest/api/2/user?username={userName}");
            return await message.Content.ReadAsAsync<JiraUserDto>();
        }

        public async Task<SprintDescriptionResultsDto> GetSprintsDescriptions(string boardId)
        {
            HttpResponseMessage message = await _httpClient.GetAsync($"jira/rest/greenhopper/1.0/sprintquery/{boardId}?includeHistoricSprints=false");
            return await message.Content.ReadAsAsync<SprintDescriptionResultsDto>();
        }

        public async Task<SprintDto> GetSprintDetails(string boardId, int sprintId)
        {
            HttpResponseMessage message = await _httpClient.GetAsync($"jira/rest/greenhopper/1.0/rapid/charts/sprintreport/?rapidViewId={boardId}&sprintId={sprintId}");
            SprintInfoDto sprintInfo = await message.Content.ReadAsAsync<SprintInfoDto>();
            return sprintInfo.Sprint;
        }

        public Task<List<JiraIssueDto>> GetIssues(params int[] sprints) => GetIssues(null, sprints);
        public Task<List<JiraIssueDto>> GetIssues(string issueAssignee, params int[] sprints) => GetIssues(null, null, issueAssignee, sprints);
        public async Task<List<JiraIssueDto>> GetIssues(List<IssueState> issueState, List<IssueState> notIssueState, string issueAssignee, params int[] sprints)
        {
            int startAt = 0;
            IssuesResultDto pageResult = null;
            List<JiraIssueDto> results = new List<JiraIssueDto>();

            do
            {
                string jql = JqlBuilder.New()
                                       .Sprints(sprints)
                                       .If(!string.IsNullOrEmpty(issueAssignee), x => x.And().Assignee(issueAssignee))
                                       .If(issueState != null && issueState.Any(), x => x.And().StatusIn(issueState))
                                       .If(notIssueState != null && notIssueState.Any(), x => x.And().StatusNotIn(notIssueState))
                                       .Ampersand()
                                       .Fields("summary")
                                       .Ampersand()
                                       .StartAt(startAt)
                                       .ToString();

                HttpResponseMessage message = await _httpClient.GetAsync($"jira/rest/api/2/search?{jql}");
                pageResult = await message.Content.ReadAsAsync<IssuesResultDto>();
                results.AddRange(pageResult.Issues);
                startAt = startAt + pageResult.Issues.Length;
            }
            while (startAt < pageResult.Total);

            return results;
        }

        public async Task<List<JiraWorkLogDto>> GetWorkLogs(int IssueId)
        {
            List<JiraWorkLogDto> workLoads = new List<JiraWorkLogDto>();
            WorkLogResultsDto workLogResultsDto = null;
            int startAt = 0;

            do
            {
                HttpResponseMessage message = await _httpClient.GetAsync($"jira/rest/api/2/issue/{IssueId}/worklog");
                workLogResultsDto = await message.Content.ReadAsAsync<WorkLogResultsDto>();
                workLoads.AddRange(workLogResultsDto.WorkLogs);
                startAt = startAt + workLogResultsDto.WorkLogs.Length;
            }
            while (startAt < workLogResultsDto.Total);

            return workLoads;
        }

        public Task LogWork(string issueId, int minutes)
        {
            return _httpClient.PostAsJsonAsync($"jira/rest/api/2/issue/{issueId}/worklog", new { timeSpent = $"{minutes}m" });
        }
    }
}
