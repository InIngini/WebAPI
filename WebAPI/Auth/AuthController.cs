using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAPI.DB;
using WebAPI.DB.CommonAppModel;
using WebAPI.BLL.Interfaces;

namespace WebAPI.Auth
{
    /// <summary>
    /// Контроллер для аутентификации
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("SwaggerLogin/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IContext Context;

        /// <summary>
        /// Инициализирует новый экземпляр класса AuthController
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public AuthController(IContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получает данные для входа в Swagger
        /// </summary>
        /// <returns>Данные для входа</returns>
        [HttpGet]
        public ActionResult<SwaggerLogin> GetSwaggerLogin()
        {
            var login = Context.SwaggerLogins.FirstOrDefault();
            return login ?? new SwaggerLogin();
        }

        /// <summary>
        /// Добавляет новые данные для входа в Swagger
        /// </summary>
        /// <param name="login">Данные для входа</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task AddNewSwaggerLogin([FromBody] SwaggerLogin login)
        {
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            Context.SwaggerLogins.Add(login);
            await Context.SaveChangesAsync();
        }
    }
}
