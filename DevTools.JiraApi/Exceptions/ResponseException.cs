using System;
using System.Net;

namespace DevTools.JiraApi.Exceptions
{
    public class ResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ResponseException(HttpStatusCode httpStatusCode) : this(string.Empty, httpStatusCode)
        {}

        public ResponseException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            StatusCode = httpStatusCode;
        }
    }
}
