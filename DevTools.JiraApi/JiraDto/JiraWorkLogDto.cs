using System;

namespace DevTools.JiraApi.JiraDto
{
    public class JiraWorkLogDto
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int TimeSpentSeconds { get; set; }
        public DateTime Created { get; set; }
        public JiraUserDto Author { get; set; }

        public JiraWorkLogDto()
        {

        }

        public JiraWorkLogDto(JiraWorkLogDto workLog)
        {
            Id = workLog.Id;
            IssueId = workLog.IssueId;
            TimeSpentSeconds = workLog.TimeSpentSeconds;
            Created = workLog.Created;
            Author = new JiraUserDto(workLog.Author);
        }
    }
}
