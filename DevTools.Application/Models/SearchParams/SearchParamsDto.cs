using DevTools.Application.Models.SearchParams.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevTools.Application.Models.SearchParams
{
    public class SearchParamsDto : ISprintFilter, IUserFilter, IIssueFilter, IDateFilter
    {
        public List<IssueState> IssueState { get; set; }
        public List<IssueState> NotIssueState { get; set; }
        public string IssueAssignee { get; set; }
        public string[] UserName { get; set; }
        public DateTime? StartFrom { get; set; }
        public SprintState? SprintState { get; set; }
    }
}
