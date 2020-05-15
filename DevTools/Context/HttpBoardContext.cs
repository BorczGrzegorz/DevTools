using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace DevTools.Context
{
    public class HttpBoardContext : IBoardContext
    {
        public string Id { get; }

        public HttpBoardContext(IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor.HttpContext == null)
            {
                return;
            }

            StringValues boardHeader = contextAccessor.HttpContext.Request.Headers["x-board"];
            if (boardHeader == StringValues.Empty)
            {
                return;
            }

            if (boardHeader.Count > 1)
            {
                throw new ArgumentException("Only one boardId is allowed");
            }

            Id = boardHeader.Single();
        }
    }
}
