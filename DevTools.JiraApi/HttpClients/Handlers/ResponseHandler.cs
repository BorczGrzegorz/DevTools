using DevTools.JiraApi.Exceptions;
using DevTools.JiraApi.Exceptions.Models;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DevTools.JiraApi.HttpClients.Handlers
{
    public class ResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            ErrorDto error;
            try
            {
                error = await response.Content.ReadAsAsync<ErrorDto>();
            }
            catch(Exception)
            {
                string message = await response.Content.ReadAsStringAsync();
                throw new ResponseException(message, response.StatusCode);
            }

            throw new JiraResponseException(error, response.StatusCode);
        }
    }
}
