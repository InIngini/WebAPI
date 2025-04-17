using System.Net;
using WebAPI.Auth;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Auth
{
    /// <summary>
    /// Middleware для базовой аутентификации
    /// </summary>
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Инициализирует новый экземпляр класса BasicAuthMiddleware
        /// </summary>
        /// <param name="next">Следующий делегат в цепочке middleware</param>
        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Обрабатывает HTTP-запрос
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса</param>
        /// <param name="authService">Сервис аутентификации</param>
        /// <returns>Задача, представляющая асинхронную операцию</returns>
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