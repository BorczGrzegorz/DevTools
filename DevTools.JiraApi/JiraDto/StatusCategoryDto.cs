using DevTools.Application.Models.SearchParams;

namespace DevTools.JiraApi.JiraDto
{
    public class StatusCategoryDto
    {
        public IssueState Id { get; set; }

        public override string ToString() => Id.ToString();
    }
}
