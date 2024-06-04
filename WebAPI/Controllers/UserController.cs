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

        //�������� ������������
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            // �������� ���������� ������
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // ���������� ������������ � ���� ������
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // ������� ���������� ������������
            return CreatedAtAction(nameof(GetUser), new { id = user.IdUser }, user);
        }
        //���� ������������
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // ����� ������������ � ���� ������
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == loginModel.Login && u.Password == loginModel.Password);

            // ���� ������������ �� ������, ������� ������
            if (user == null)
            {
                return Unauthorized();
            }

            // �������� JWT-������
            var token = _tokenService.CreateToken(user);

            // ������� ������
            return Ok(new { token });
        }
        //��������� ������������ �� id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            // ��������� ������������ �� ���� ������
            var user = await _context.Users.FindAsync(id);

            // ���� ������������ �� ������, ������� ������
            if (user == null)
            {
                return NotFound();
            }

            // ������� ������������
            return Ok(user);
        }

    }
}
