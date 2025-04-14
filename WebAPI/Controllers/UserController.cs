using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.BLL.Token;
using WebAPI;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserController"/>.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///       "login": "username2",
        ///       "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <param name="loginData">Данные для регистрации пользователя.</param>
        /// <returns>Результат создания пользователя.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody] LoginData loginData)
        {
            var createdUser = await _userService.CreateUser(loginData);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Авторизирует пользователя.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///       "login": "username",
        ///       "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <param name="loginData">Данные для авторизации пользователя.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Токен пользователя, полученный в результате авторизации.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserTokenData), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginData loginData,CancellationToken cancellationToken)
        {
            var userToken = await _userService.Login(loginData,cancellationToken);

            //var token = user.Token;

            return Ok(userToken);
        }

        /// <summary>
        /// Получает информацию о пользователе по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Информация о пользователе с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser(int id,CancellationToken cancellationToken)
        {
            var user = await _userService.GetUser(id,cancellationToken);

            return Ok(user);
        }

    }
}

