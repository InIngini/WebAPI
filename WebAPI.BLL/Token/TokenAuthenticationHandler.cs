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
    public class TokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITokenValidator _tokenValidator;

        public TokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenValidator tokenValidator)
            : base(options, logger, encoder, clock)
        {
            _tokenValidator = tokenValidator;
            
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var request = Request;
            string authorizationHeader = Request.Headers["Authorization"];
            // Получение токена из заголовка запроса
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Валидация токена
            int userId = _tokenValidator.ValidateToken(token);

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
