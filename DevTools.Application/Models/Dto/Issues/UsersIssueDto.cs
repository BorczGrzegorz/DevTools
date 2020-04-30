using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Application.Models.Dto.Issues
{
    public class UsersIssueDto : Dictionary<string, IEnumerable<IssueWorkLogDto>>
    {
        public UsersIssueDto(IDictionary<string, IEnumerable<IssueWorkLogDto>> dictionary) : base(dictionary)
        {}
    }
}
