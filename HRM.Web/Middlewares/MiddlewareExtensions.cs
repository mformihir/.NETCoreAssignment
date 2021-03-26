using Microsoft.AspNetCore.Builder;

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
