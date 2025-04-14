using System.Net;
using WebAPI.Auth;

namespace WebAPI.Auth
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        // Конструктор получает ТОЛЬКО RequestDelegate
        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IAuthService получаем через DI в InvokeAsync
        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader) ||
                    !await authService.IsAuthorized(authHeader))
                {
                    context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Swagger UI\"";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
            }

            await _next(context);
        }
    }
}