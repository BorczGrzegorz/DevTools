using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.JiraDto
{
    public class JiraIssueDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public IssueFields Fields { get; set; }

        public override string ToString() => Key + " " + (Fields != null ? Fields.ToString() + " " : string.Empty) + Id;
    }
}
