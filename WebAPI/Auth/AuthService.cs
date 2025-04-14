using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using WebAPI.DB;

namespace WebAPI.Auth
{
    public class AuthService : IAuthService
    {
        private IContext _context;
        private const string UsernameDefault = "your_username";
        private const string PasswordDefault = "your_password";

        public AuthService(IContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAuthorized(string authHeader)
        {
            // Проверяем формат заголовка
            if (authHeader.StartsWith("Basic "))
            {
                var forVerif = await _context.SwaggerLogins.FirstOrDefaultAsync();

                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var credentialBytes = Convert.FromBase64String(encodedCredentials);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

                if (credentials.Length == 2)
                {
                    var username = credentials[0];
                    var password = credentials[1];

                    return username == (forVerif != null ? forVerif.Login : UsernameDefault)
                        && password == (forVerif != null ? forVerif.Password : PasswordDefault);
                }
            }

            return false;
        }
    }
}
