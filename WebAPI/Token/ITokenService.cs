namespace WebAPI.Token
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
