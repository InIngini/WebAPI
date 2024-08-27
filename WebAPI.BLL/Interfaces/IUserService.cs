using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(LoginData loginData);
        Task<UserTokenData> Login(LoginData loginData,CancellationToken cancellationToken);
        Task<User> GetUser(int id, CancellationToken cancellationToken);
    }

}
