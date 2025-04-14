using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace WebAPI.Errors
{
    /// <summary>
    /// Middleware для обработки ошибок в приложении.
    /// Позволяет перехватывать исключения, возникающие при обработке HTTP-запросов,
    /// и возвращать информативные ответы клиенту.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ErrorHandlingMiddleware"/>.
        /// </summary>
        /// <param name="next">Делегат следующего компонента в цепочке обработки запросов.</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Метод для обработки входящих HTTP-запросов.
        /// Перехватывает исключения, возникающие в процессе выполнения запроса.
        /// </summary>
        /// <param name="context">Контекст текущего HTTP-запроса.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Обрабатывает возникшее исключение и формирует ответ с информацией об ошибке.
        /// </summary>
        /// <param name="context">Контекст текущего HTTP-запроса.</param>
        /// <param name="exception">Возникшее исключение.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var apiError = new ApiError
            {
                Message = "Произошла ошибка при выполнении запроса.",
                Code = exception.GetType().Name,
                Details = new List<string> { exception.Message }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(apiError));
        }
    }

}
