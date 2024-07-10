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
using WebAPI.DAL.Entities;

namespace WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Создание пользователя
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await _userService.CreateUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.IdUser }, user);
        }

        //Авторизация пользователя
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            var userToken = await _userService.Login(loginData);

            //var token = user.Token;

            return Ok(new { userToken });
        }

        //Получить пользователя
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);

            return Ok(user);
        }

    }
}

