using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace JWT.Core
{
    public class PreflightRequestsHandler : DelegatingHandler
    {
        /// <summary>
        /// In the CORS workflow, before sending a DELETE, PUT or POST request, the client sends an OPTIONS request to check that the domain from which the request originates is the same as the server.
        /// If the request domain and server domain are not the same, then the server must include various access headers that describe which domains have access.
        ///  To enable access to all domains, we just respond with an origin header (Access-Control-Allow-Origin) with an asterisk to enable access for all.
        ///
        ///The Access-Control-Allow-Headers header describes which headers the API can accept/is expecting to receive.
        /// The Access-Control-Allow-Methods header describes which HTTP verbs are supported/permitted.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("Origin") && request.Method.Method == "OPTIONS")
            {
                var response = new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Authorization");
                response.Headers.Add("Access-Control-Allow-Methods", "*");
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}