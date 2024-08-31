using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DB.Entities;
using Microsoft.Extensions.Configuration;

namespace WebAPI.BLL.Token
{
    /// <summary>
    /// Сервис для работы с токенами JWT.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TokenService"/>.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Создает JWT токен для указанного пользователя.
        /// </summary>
        /// <param name="user">Пользователь, для которого создается токен.</param>
        /// <returns>JWT токен.</returns>
        public string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                // Добавьте другие необходимые утверждения
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:TokenExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
