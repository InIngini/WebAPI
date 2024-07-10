using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Token;
using WebAPI.DAL.Entities;
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

        public async Task<User> CreateUser(User user)
        {
            var validationContext = new ValidationContext(user);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
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
