using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.JiraDto
{
    public class IssuesResultDto
    {
        public int startAt { get; set; }
        public int Total { get; set; }
        public JiraIssueDto[] Issues { get; set; }
    }
}
