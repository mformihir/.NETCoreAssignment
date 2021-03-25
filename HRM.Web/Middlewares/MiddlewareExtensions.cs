using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Web.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMeasureResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MeasureResponseTimeMiddleware>();
        }

        public static void UseCustomExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlingMiddleware>();
        }
    }
}
