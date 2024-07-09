using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Token
{
    public interface ITokenValidator
    {
        int ValidateToken(string token);
    }
}
