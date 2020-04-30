using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Application.Models.Dto.WorkLog
{
    public class WorkLogDto
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int TimeSpentSeconds { get; set; }
        public DateTime Created { get; set; }
    }
}
