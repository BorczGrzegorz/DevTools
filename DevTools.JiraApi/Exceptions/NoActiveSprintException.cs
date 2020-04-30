using System;

namespace DevTools.JiraApi.Exceptions
{
    public class NoActiveSprintException : Exception
    {
        public NoActiveSprintException(string message): base(message)
        {

        }
    }
}
