using Microsoft.AspNetCore.Builder;
using WebSwIT.PresentationLayer.Middlewares;

namespace WebSwIT.PresentationLayer.Extentions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionMiddleware));
        }

        public static void ConfigureLogHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(LogMiddleware));
        }
    }
}
