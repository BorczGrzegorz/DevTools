using DevTools.Application.Models.SearchParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.JiraApi.Builders
{
    internal class JqlBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private JqlBuilder()
        {
            _stringBuilder.Append("jql=");
        }

        internal static JqlBuilder New() => new JqlBuilder();

        internal JqlBuilder Assignee(string userName)
        {
            _stringBuilder.Append($"assignee={userName} ");
            return this;
        }

        internal JqlBuilder AssigneeToCurrentUser()
        {
            _stringBuilder.Append("assignee=currentUser() ");
            return this;
        }

        internal JqlBuilder Fields(params string[] fields)
        {
            _stringBuilder.Append($"fields={string.Join(",", fields.Select(x => x.ToLower()))}");
            return this;
        }

        internal JqlBuilder Sprints(params int[] sprintsIds)
        {
            _stringBuilder.Append("(");
            for (int i = 0; i < sprintsIds.Length - 1; i++)
            {
                Sprint(sprintsIds[i]).Or();
            }
            Sprint(sprintsIds[sprintsIds.Length - 1]);
            _stringBuilder.Append(") ");
            return this;
        }

        internal JqlBuilder Sprint(int sprintId)
        {
            _stringBuilder.Append($"sprint={sprintId} ");
            return this;
        }

        internal JqlBuilder StatusIn(List<IssueState> value)
        {
            string joinedValues = string.Join(",", value.Select(x => x.ToString().Replace('_', ' ')));
            _stringBuilder.Append($"status IN ({joinedValues}) ");
            return this;
        }

        internal JqlBuilder StatusNotIn(List<IssueState> value)
        {
            string joinedValues = string.Join(",", value.Select(x => x.ToString().Replace('_', ' ')));
            _stringBuilder.Append($"status NOT IN ({joinedValues}) ");
            return this;
        }

        internal JqlBuilder Or()
        {
            _stringBuilder.Append("OR ");
            return this;
        }

        internal JqlBuilder And()
        {
            _stringBuilder.Append("AND ");
            return this;
        }

        internal JqlBuilder Ampersand()
        {
            _stringBuilder.Append('&');
            return this;
        }

        internal JqlBuilder StartAt(int startAt)
        {
            _stringBuilder.Append($"startAt={startAt}");
            return this;
        }

        public override string ToString() => _stringBuilder.ToString();
    }
}
