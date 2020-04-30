using DevTools.Application.Models.Dto.Issues;
using DevTools.Application.Models.Dto.WorkLog;
using DevTools.Application.Models.SearchParams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevTools.Application.Abstract
{
    public interface IWorkLogQuery
    {
        Task<UsersWorkLogDto> GetWorkLogs(SearchParamsDto searchParams);
        Task<UsersIssueDto> GetIssues(SearchParamsDto searchParams);
        Task<List<IssueDto>> GetIssues(IssueSearchParamsDto searchParams);
        Task<UsersDatesSummary> GetUserDatesSummary(SearchParamsDto searchParams);
    }
}
