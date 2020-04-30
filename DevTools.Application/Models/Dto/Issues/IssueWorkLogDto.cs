using DevTools.Application.Models.Dto.WorkLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.Application.Models.Dto.Issues
{
    public class IssueWorkLogDto : IssueDto
    {
        public int TimeSpentSeconds { get => Worklogs.Sum(x => x.TimeSpentSeconds); }
        public List<WorkLogDto> Worklogs { get; set; }
    }
}
