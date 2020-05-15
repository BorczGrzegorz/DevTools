using DevTools.JiraApi.Exceptions;
using DevTools.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DevTools
{
    public static class HttpContextExtensions
    {
        public static Task Error(this HttpContext context, JiraResponseException ex)
        {
            ErrorCode errorCode = ErrorCode.UNKNOWN;
            if (ex.Error.Errors != null && ex.Error.Errors.Any())
            {
                if (ex.Error.Errors.ContainsKey("rapidViewId"))
                {
                    errorCode = ErrorCode.BAD_BOARD_ID;
                }
            }

            return Status(context, ex.StatusCode, errorCode);
        }

        public static Task Error(this HttpContext context, ResponseException ex)
        => Status(context, ex.StatusCode, ex.Message);

        public static Task BadRequest(this HttpContext context, ErrorCode errorCode)
        => Status(context, HttpStatusCode.BadRequest, errorCode);

        public static Task Status(this HttpContext context, HttpStatusCode status, ErrorCode errorCode)
        {
            var errorDto = new ErrorDto(errorCode);
            return WriteJsonAsync(context, status, errorDto);
        }

        public static Task InternalServerError(this HttpContext context, string message)
        => Status(context, HttpStatusCode.InternalServerError, message);

        public static Task Status(this HttpContext context, HttpStatusCode status, string message)
        {
            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(message);
        }
        private static Task WriteJsonAsync(this HttpContext context, HttpStatusCode code, object model)
        {
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            var options = (IOptions<MvcNewtonsoftJsonOptions>)context.RequestServices.GetService(typeof(IOptions<MvcNewtonsoftJsonOptions>));
            return context.Response.WriteAsync(JsonConvert.SerializeObject(model, options.Value.SerializerSettings));
        }
    }
}
