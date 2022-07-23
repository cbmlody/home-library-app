using HomeLibraryAPI.Middleware;

using Microsoft.AspNetCore.Builder;

namespace HomeLibraryAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
