using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using WebAPI.DB;
using WebAPI.BLL.Interfaces;

namespace WebAPI.Auth
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IContext Context;
        private const string UsernameDefault = "your_username";
        private const string PasswordDefault = "your_password";

        /// <summary>
        /// Инициализирует новый экземпляр класса AuthService
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public AuthService(IContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Проверяет авторизацию пользователя
        /// </summary>
        /// <param name="authHeader">Заголовок авторизации</param>
        /// <returns>Результат проверки авторизации</returns>
        public async Task<bool> IsAuthorized(string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader))
            {
                return false;
            }

            var authValues = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Split(' ')[1])).Split(':');
            var login = authValues[0];
            var password = authValues[1];

            var user = await Context.SwaggerLogins.FirstOrDefaultAsync();

            return login == (user != null ? user.Login : UsernameDefault)
                    && password == (user != null ? user.Password : PasswordDefault);
        }
    }
}
