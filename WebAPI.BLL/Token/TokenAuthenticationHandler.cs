using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Token
{
    /// <summary>
    /// Обработчик аутентификации токенов.
    /// </summary>
    public class TokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITokenValidator TokenValidator;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TokenAuthenticationHandler"/>.
        /// </summary>
        /// <param name="options">Параметры схемы аутентификации.</param>
        /// <param name="logger">Логгер для обработки журналов.</param>
        /// <param name="encoder">Кодировщик URL.</param>
        /// <param name="clock">Часы системы.</param>
        /// <param name="tokenValidator">Сервис валидации токенов.</param>
        public TokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenValidator tokenValidator)
            : base(options, logger, encoder, clock)
        {
            TokenValidator = tokenValidator;
            
        }

        /// <summary>
        /// Обрабатывает аутентификацию.
        /// </summary>
        /// <returns>Результат аутентификации.</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Получение токена из заголовка запроса
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Валидация токена
            int userId = await TokenValidator.ValidateToken(token);

            if (userId > 0)
            {
                // Создание claims identity с идентификатором пользователя
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, userId.ToString()) }, Scheme.Name));

                // Возврат успешного результата аутентификации
                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
            }
            else
            {
                // Возврат неуспешного результата аутентификации
                return AuthenticateResult.Fail("Invalid token");
            }
        }
    }

}
