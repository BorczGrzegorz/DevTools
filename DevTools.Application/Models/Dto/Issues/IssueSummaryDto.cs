namespace DevTools.Application.Models.Dto.Issues
{
    public class IssueSummaryDto
    {
        public int IssueId { get; set; }
        public string Key { get; set; }
        public string Summary { get; set; }
        public int TimeSpentSeconds { get; set; }
    }
}
