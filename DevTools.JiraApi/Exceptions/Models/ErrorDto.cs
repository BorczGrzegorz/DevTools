using System.Collections.Generic;

namespace DevTools.JiraApi.Exceptions.Models
{
    public class ErrorDto
    {
        public string[] ErrorMessages { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }
}
