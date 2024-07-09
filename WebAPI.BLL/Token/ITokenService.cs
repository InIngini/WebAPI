using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Token
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
