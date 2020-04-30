using DevTools.Application.Models.SearchParams;
using DevTools.JiraApi.JiraDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevTools.JiraApi.Abstract
{
    public interface IJiraWebClient
    {
        Task<string> GetUserName();
        Task<JiraUserDto> GetUser(string name);
        Task<SprintDescriptionResultsDto> GetSprintsDescriptions(string boardId);
        Task<SprintDto> GetSprintDetails(string boardId, int sprintId);
        Task<List<JiraIssueDto>> GetIssues(params int[] sprints);
        Task<List<JiraIssueDto>> GetIssues(string issueAssignee, params int[] sprints);
        Task<List<JiraIssueDto>> GetIssues(List<IssueState> issueState, List<IssueState> notIssueState, string issueAssignee, params int[] sprints);
        Task<List<JiraWorkLogDto>> GetWorkLogs(int IssueId);
        Task LogWork(string issueId, int minutes);
    }
}
