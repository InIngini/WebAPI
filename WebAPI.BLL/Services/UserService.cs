using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Token;
using WebAPI.DB.Entities;
using WebAPI.DB;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebAPI.Errors;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления пользователями.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IContext Context;
        private readonly ITokenService TokenService;
        private readonly IMapper Mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserService"/>.
        /// </summary>
        /// <param name="context">Контекст бд.</param>
        /// <param name="tokenService">Сервис для работы с токенами.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public UserService(IContext context, ITokenService tokenService, IMapper mapper)
        {
            Context = context;
            TokenService = tokenService;
            Mapper = mapper;
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
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }

            // Используем AutoMapper для маппинга LoginData в User
            User user = Mapper.Map<User>(loginData);

            if (await Context.Users.Where(u => u.Login == user.Login).FirstOrDefaultAsync() != null)
            {
                throw new ArgumentException(TypesOfErrors.UserFound(loginData.Login));
            }

            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Выполняет вход для пользователя и возвращает токен.
        /// </summary>
        /// <param name="loginData">Данные для входа пользователя.</param>
        /// <returns>Данные пользователя с токеном.</returns>
        /// <exception cref="UnauthorizedAccessException">Если пользователь не найден.</exception>
        public async Task<UserTokenData> Login(LoginData loginData, CancellationToken cancellationToken)
        {
            var user = await Context.Users.Where(u => u.Login == loginData.Login && u.Password == loginData.Password)
                                              .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new UnauthorizedAccessException(TypesOfErrors.UserNotFound(loginData.Login));
            }

            var token = TokenService.CreateToken(user);

            // Возвращаем пользователя с токеном
            var userTokenData = Mapper.Map<UserTokenData>(user); // 'user' - это объект класса User
            userTokenData.Token = token; // Устанавливаем токен

            return userTokenData;
        }

        /// <summary>
        /// Получает пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Запрашиваемый пользователь.</returns>
        /// <exception cref="KeyNotFoundException">Если пользователь не найден.</exception>
        public async Task<User> GetUser(int id, CancellationToken cancellationToken)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Пользователь", 1));
            }

            return user;
        }
    }


}
