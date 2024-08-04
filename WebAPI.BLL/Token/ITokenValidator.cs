using WebAPI.DB.Entities;

namespace WebAPI.BLL.Token
{
    /// <summary>
    /// Интерфейс для валидации токенов.
    /// </summary>
    public interface ITokenValidator
    {
        /// <summary>
        /// Проверяет действительность указанного токена.
        /// </summary>
        /// <param name="token">JWT токен, который необходимо проверить.</param>
        /// <returns>Идентификатор пользователя, связанного с токеном, если токен действителен; иначе 0.</returns>
        int ValidateToken(string token);
    }

}
