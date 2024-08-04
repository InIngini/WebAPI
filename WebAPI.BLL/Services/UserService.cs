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
using AutoMapper;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления пользователями.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserService"/>.
        /// </summary>
        /// <param name="unitOfWork">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="tokenService">Сервис для работы с токенами.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public UserService(IUnitOfWork unitOfWork, ITokenService tokenService,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="loginData">Данные для создания пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна или пользователь уже существует.</exception>
        public async Task<User> CreateUser(LoginData loginData)
        {
            var validationContext = new ValidationContext(loginData);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(loginData, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            // Используем AutoMapper для маппинга LoginData в User
            User user = _mapper.Map<User>(loginData);

            if (_unitOfWork.Users.Find(u => u.Login==user.Login).FirstOrDefault() != null)
            {
                throw new ArgumentException("Такой уже есть");
            }
            
            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();

            return user;
        }

        /// <summary>
        /// Выполняет вход для пользователя и возвращает токен.
        /// </summary>
        /// <param name="loginData">Данные для входа пользователя.</param>
        /// <returns>Данные пользователя с токеном.</returns>
        /// <exception cref="UnauthorizedAccessException">Если пользователь не найден.</exception>
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
            var userTokenData = _mapper.Map<UserTokenData>(user); // 'user' - это объект класса User
            userTokenData.Token = token; // Устанавливаем токен

            return userTokenData;
        }

        /// <summary>
        /// Получает пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Запрашиваемый пользователь.</returns>
        /// <exception cref="KeyNotFoundException">Если пользователь не найден.</exception>
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
