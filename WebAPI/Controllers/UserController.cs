using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;

namespace WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserController : ControllerBase
    {
        //private readonly ILogger<UserController> _logger;

        //public UserController(ILogger<UserController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly Context _context;

        public UserController(Context context)
        {
            _context = context;
        }
        //[HttpGet]
        //public IEnumerable<User> Get()
        //{
        //    return Enumerable.Range(1, 1).Select(index => new User
        //    {
        //        IdUser = index,
        //        Login = "ingini",
        //        Password = string.Empty,
        //    })
        //    .ToArray();
        //}

        //Создание пользователя
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение пользователя в базе данных
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Возврат созданного пользователя
            return CreatedAtAction(nameof(GetUser), new { id = user.IdUser }, user);
        }
        //Вход пользователя
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // Поиск пользователя в базе данных
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == loginModel.Login && u.Password == loginModel.Password);

            // Если пользователь не найден, вернуть ошибку
            if (user == null)
            {
                return Unauthorized();
            }

            // Создание JWT-токена
            var token = _tokenService.CreateToken(user);

            // Возврат токена
            return Ok(new { token });
        }
        //Получение пользователя по id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            // Получение пользователя из базы данных
            var user = await _context.Users.FindAsync(id);

            // Если пользователь не найден, вернуть ошибку
            if (user == null)
            {
                return NotFound();
            }

            // Возврат пользователя
            return Ok(user);
        }

    }
}
