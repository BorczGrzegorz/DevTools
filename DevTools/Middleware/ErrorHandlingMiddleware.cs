using DevTools.Exceptions;
using DevTools.JiraApi.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DevTools.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BoardIdNotFoundException)
            {
                await context.BadRequest(Models.ErrorCode.BOARD_ID_UNSPECIFIED);
            }
            catch (JiraResponseException ex)
            {
                await context.Error(ex);
            }
            catch (ResponseException ex)
            {
                await context.Error(ex);
            }
            catch (Exception ex)
            {
                await context.InternalServerError(ex.Message);
            }
        }
    }
}
