namespace WebAPI.Token
{
    public interface ITokenValidator
    {
        int ValidateToken(string token);
    }
}
