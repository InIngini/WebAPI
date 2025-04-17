namespace WebAPI.Auth
{
    /// <summary>
    /// Интерфейс сервиса аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Проверяет авторизацию пользователя
        /// </summary>
        /// <param name="authHeader">Заголовок авторизации</param>
        /// <returns>Результат проверки авторизации</returns>
        Task<bool> IsAuthorized(string authHeader);
    }
}
