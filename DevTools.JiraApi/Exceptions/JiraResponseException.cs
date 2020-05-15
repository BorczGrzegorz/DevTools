using DevTools.JiraApi.Exceptions.Models;
using System.Net;

namespace DevTools.JiraApi.Exceptions
{
    public class JiraResponseException : ResponseException
    {
        public ErrorDto Error { get; }

        public JiraResponseException(ErrorDto errorDto, HttpStatusCode statusCode) : base(statusCode)
        {
            Error = errorDto;
        }
    }
}
