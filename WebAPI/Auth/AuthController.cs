using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAPI.DB;
using WebAPI.DB.CommonAppModel;

namespace WebAPI.Auth
{
    /// <summary>
    /// Контроллер для управления аутентификацией в сваггере
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("SwaggerLogin/[controller]")]
    public class AuthController : Controller
    {
        private IContext Context { get; }
        public AuthController(IContext context)
        { 
            Context = context;
        }

        [HttpGet]
        public async Task<SwaggerLogin> GetSwaggerLogin()
        {
            return await Context.SwaggerLogins.FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task AddNewSwaggerLogin(SwaggerLogin swaggerLogin)
        {
            var swaggerLogins = await Context.SwaggerLogins.ToListAsync();
            Context.SwaggerLogins.RemoveRange(swaggerLogins);

            Context.SwaggerLogins.Add(swaggerLogin);
            await Context.SaveChangesAsync();
        }
    }
}
