using Newtonsoft.Json;

namespace SchedulerApp
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SchedulerErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public SchedulerErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = JsonConvert.SerializeObject(new
            {
                action = "error"
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SchedulerErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseSchedulerErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SchedulerErrorMiddleware>();
        }
    }
}
