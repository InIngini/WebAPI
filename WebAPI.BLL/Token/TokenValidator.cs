using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DB.Entities;
using WebAPI.DB;
using Microsoft.Extensions.Configuration;
using WebAPI.Errors;
using WebAPI.BLL.Errors;

namespace WebAPI.BLL.Token
{
    /// <summary>
    /// Сервис для валидации JWT токенов.
    /// </summary>
    public class TokenValidator : ITokenValidator
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TokenValidator"/>.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <param name="context">Контекст базы данных.</param>
        public TokenValidator(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        /// <summary>
        /// Валидирует указанный токен и возвращает идентификатор пользователя.
        /// </summary>
        /// <param name="token">JWT токен.</param>
        /// <returns>ID пользователя, если токен валиден; 0, если токен невалиден.</returns>
        public int ValidateToken(string token)
        {
            if (token != "")
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);

                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };

                    ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                    var userName = claimsPrincipal.FindFirstValue(ClaimTypes.Name);// Получаем логин из токена

                    var user = _context.Users.FirstOrDefault(u => u.Login == userName);// Получаем айдишник этого токена

                    if (user != null)
                    {
                        return user.Id; // Возвращаем айди пользователя
                    }
                    else
                    {
                        // Если пользователь не найден, выбрасываем исключение с помощью TypesOfErrors
                        throw new KeyNotFoundException(TypesOfErrors.UserNotFound(userName)); // Передаем логин, чтобы передать больше информации
                    }
                }
                catch (Exception ex)
                {
                    //return 0;
                    // Получаем ApiError через метод TokenNotValid
                    var apiError = TypesOfErrors.TokenNotValid(ex);

                    // Бросаем новое ApiException с объектом ApiError
                    throw new ApiException(apiError);
                }
            }
            return 0;
        }
    }
}
