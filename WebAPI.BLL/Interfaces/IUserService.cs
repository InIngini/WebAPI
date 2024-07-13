using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(LoginData loginData);
        Task<UserTokenData> Login(LoginData loginData);
        Task<User> GetUser(int id);
    }

}
