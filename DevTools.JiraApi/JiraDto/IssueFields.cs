namespace DevTools.JiraApi.JiraDto
{
    public class IssueFields
    {
        public string Summary { get; set; }
        public StatusCategoryDto Status { get; set; }

        public override string ToString() => Summary + " " + Status?.ToString();
    }
}
