namespace WebAPI.Auth
{
    public interface IAuthService
    {
        Task<bool> IsAuthorized(string authHeader);
    }
}
