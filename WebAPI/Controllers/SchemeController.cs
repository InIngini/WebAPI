using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Errors;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления схемами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/Book/[controller]")]
    public class SchemeController : ControllerBase
    {
        private readonly ISchemeService SchemeService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SchemeController"/>.
        /// </summary>
        /// <param name="schemeService">Сервис для работы с схемами.</param>
        public SchemeController(ISchemeService schemeService)
        {
            SchemeService = schemeService;
        }

        /// <summary>
        /// Создает новую схему.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///         "BookId": "1",
        ///         "NameScheme": "Новое имя"
        ///     }
        ///
        /// </remarks>
        /// <param name="schemedata">Данные о схеме, которую необходимо создать.</param>
        /// <returns>Результат создания схемы.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Scheme), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateScheme([FromBody] SchemeData schemedata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NotValidModel(ModelState));
            }
            
            var createdScheme = await SchemeService.CreateScheme(schemedata);

            return CreatedAtAction(nameof(GetScheme), new { id = createdScheme.Id }, createdScheme);

        }

        /// <summary>
        /// Обновляет схему, добавляя связи.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <param name="idConnection">Идентификатор связи для добавления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Обновлённая схема.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Scheme), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateScheme(int id, [FromBody] int idConnection,CancellationToken cancellationToken)
        {
            var scheme = await SchemeService.UpdateScheme(id,idConnection);

            if (scheme == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Схема", 0));
            }

            return Ok(scheme);
        }

        /// <summary>
        /// Удаляет схему по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы для удаления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Результат удаления схемы.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheme(int id,CancellationToken cancellationToken)
        {
            await SchemeService.DeleteScheme(id);

            return Ok();
        }

        /// <summary>
        /// Получает схему по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Схема с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Scheme), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetScheme(int id, CancellationToken cancellationToken)
        {
            var scheme = await SchemeService.GetScheme(id,cancellationToken);

            if (scheme == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Схема", 0));
            }

            return Ok(scheme);
        }

        /// <summary>
        /// Получает все схемы по заданному идентификатору книги.
        /// </summary>
        /// <param name="id">Идентификатор для получения всех схем книги.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех схем для указанного идентификатора книги.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<Scheme>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllScheme([FromBody] int id, CancellationToken cancellationToken)
        {
            var schemes = await SchemeService.GetAllSchemes(id,cancellationToken);

            if (schemes == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Схемы", 3));
            }

            return Ok(schemes);
        }
    }
}

