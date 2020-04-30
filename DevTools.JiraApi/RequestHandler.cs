using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevTools.JiraApi
{
    public class RequestHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
                                                               CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            request.Headers.Add("Cookie", context.Request.Headers["x-cookie"].ToString());            
            return base.SendAsync(request, cancellationToken);
        }
    }
}
