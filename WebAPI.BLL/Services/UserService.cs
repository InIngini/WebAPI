using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Token;
using WebAPI.DB.Entities;
using WebAPI.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public UserService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<User> CreateUser(LoginData loginData)
        {
            var validationContext = new ValidationContext(loginData);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(loginData, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            User user = new User()
            {
                Login =loginData.Login,
                Password=loginData.Password
            };

            if(_unitOfWork.Users.Find(u => u.Login==user.Login).FirstOrDefault() != null)
            {
                throw new ArgumentException("Такой уже есть");
            }
            
            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();

            return user;
        }

        public async Task<UserTokenData> Login(LoginData loginData)
        {
            var user = _unitOfWork.Users.Find(u => u.Login == loginData.Login && u.Password == loginData.Password)
                                              .FirstOrDefault();

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var token = _tokenService.CreateToken(user);

            // Возвращаем пользователя с токеном
            return new UserTokenData
            {
                IdUser = user.IdUser,
                Login = user.Login,
                Password = user.Password,
                Token = token
            };
        }

        public async Task<User> GetUser(int id)
        {
            var user = _unitOfWork.Users.Get(id);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            return user;
        }
    }


}
