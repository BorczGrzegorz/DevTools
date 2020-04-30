using DevTools.Application.Models.SearchParams;
using DevTools.Application.Models.SearchParams.Abstract;
using DevTools.JiraApi.Abstract;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.JiraApi
{
    internal static class JiraWebClientExtensions
    {
        internal static Task<List<JiraIssueDto>> GetIssues(this IJiraWebClient client,
                                                           IIssueFilter searchParamsDto, 
                                                           IEnumerable<SprintDto> sprints)
        {
            return client.GetIssues(searchParamsDto.IssueState, 
                                    searchParamsDto.NotIssueState,
                                    searchParamsDto.IssueAssignee, 
                                    sprints.Select(x => x.Id).ToArray());
        }

        internal static async Task<IEnumerable<JiraWorkLogDto>> GetWorkLoads(this IJiraWebClient jiraWebClient, IEnumerable<JiraIssueDto> issues)
        {
            IEnumerable<Task<List<JiraWorkLogDto>>> workLoadsTasks = issues.Select(x => jiraWebClient.GetWorkLogs(x.Id));
            List<JiraWorkLogDto>[] workloadsArray = await Task.WhenAll(workLoadsTasks);
            return workloadsArray.FlatMap();
        }
    }
}
