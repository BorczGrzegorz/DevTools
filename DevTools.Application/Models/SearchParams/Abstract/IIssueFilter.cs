using System.Collections.Generic;

namespace DevTools.Application.Models.SearchParams.Abstract
{
    public interface IIssueFilter
    {
        List<IssueState> IssueState { get; set; }
        List<IssueState> NotIssueState { get; set; }
        string IssueAssignee { get; set; }
    }
}
