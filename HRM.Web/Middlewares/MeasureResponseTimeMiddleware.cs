using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Web.Middlewares
{
    public class MeasureResponseTimeMiddleware
    {
        private const string ResponseHeader = "X-Response-Time-ms";

        private readonly RequestDelegate _next;
        public MeasureResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() =>
            {
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                context.Response.Headers[ResponseHeader] = responseTimeForCompleteRequest.ToString();
                return Task.CompletedTask;
            });
            return _next(context);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMeasureResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MeasureResponseTimeMiddleware>();
        }
    }
}
