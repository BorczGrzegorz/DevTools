using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.JiraDto
{
    public class WorkLogResultsDto
    {
         public int StartAt { get; set; }
         public int Total { get; set; }
         public JiraWorkLogDto[] WorkLogs { get; set; }
    }
}
