using DevTools.Application.Models.SearchParams.Abstract;
using System;
using System.Collections.Generic;

namespace DevTools.Application.Models.SearchParams
{
    public class IssueSearchParamsDto : ISprintFilter, IIssueFilter, IDateFilter
    {
        public SprintState? SprintState { get; set; }
        public List<IssueState> IssueState { get; set; }
        public List<IssueState> NotIssueState { get; set; }
        public string IssueAssignee { get; set; }
        public DateTime? StartFrom { get; set; }
    }
}
