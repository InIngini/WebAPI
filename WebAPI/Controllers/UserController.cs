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
    /// ���������� ��� ���������� ��������������.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="UserController"/>.
        /// </summary>
        /// <param name="userService">������ ��� ������ � ��������������.</param>
        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        /// <summary>
        /// ������������ ������ ������������.
        /// </summary>
        /// <remarks>
        /// ������ ��� �������������: 
        /// 
        ///     {
        ///       "login": "username2",
        ///       "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <param name="loginData">������ ��� ����������� ������������.</param>
        /// <returns>��������� �������� ������������.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody] LoginData loginData)
        {
            var createdUser = await UserService.CreateUser(loginData);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// ������������ ������������.
        /// </summary>
        /// <remarks>
        /// ������ ��� �������������: 
        /// 
        ///     {
        ///       "login": "username",
        ///       "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <param name="loginData">������ ��� ����������� ������������.</param>
        /// <param name="cancellationToken">����� ��� ������ �������.</param>
        /// <returns>����� ������������, ���������� � ���������� �����������.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserTokenData), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginData loginData,CancellationToken cancellationToken)
        {
            var userToken = await UserService.Login(loginData,cancellationToken);

            //var token = user.Token;

            return Ok(userToken);
        }

        /// <summary>
        /// �������� ���������� � ������������ �� ��� ��������������.
        /// </summary>
        /// <param name="id">������������� ������������.</param>
        /// <param name="cancellationToken">����� ��� ������ �������.</param>
        /// <returns>���������� � ������������ � ��������� ���������������.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser(int id,CancellationToken cancellationToken)
        {
            var user = await UserService.GetUser(id,cancellationToken);

            return Ok(user);
        }

    }
}

