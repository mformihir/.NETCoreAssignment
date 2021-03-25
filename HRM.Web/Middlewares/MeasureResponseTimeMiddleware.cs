using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HRM.Web.Middlewares
{
    public class MeasureResponseTimeMiddleware
    {
        private const string ResponseHeader = "X-Response-Time-ms";

        private readonly RequestDelegate _next;
        private readonly ILogger<MeasureResponseTimeMiddleware> _logger;
        public MeasureResponseTimeMiddleware(RequestDelegate next, ILogger<MeasureResponseTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() =>
            {
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                _logger.LogInformation("Response Time: " + responseTimeForCompleteRequest);
                return Task.CompletedTask;
            });
            return _next(context);
        }
    }
}
