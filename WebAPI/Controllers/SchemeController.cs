using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;

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
        private readonly ISchemeService _schemeService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SchemeController"/>.
        /// </summary>
        /// <param name="schemeService">Сервис для работы с схемами.</param>
        public SchemeController(ISchemeService schemeService)
        {
            _schemeService = schemeService;
        }

        /// <summary>
        /// Создает новую схему.
        /// </summary>
        /// <param name="schemedata">Данные о схеме, которую необходимо создать.</param>
        /// <returns>Результат создания схемы.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateScheme([FromBody] SchemeData schemedata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdScheme = await _schemeService.CreateScheme(schemedata);

            return CreatedAtAction(nameof(GetScheme), new { id = createdScheme.Id }, createdScheme);

        }

        /// <summary>
        /// Обновляет схему, добавляя связи.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <param name="idConnection">Идентификатор связи для добавления.</param>
        /// <returns>Обновлённая схема.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScheme(int id, [FromBody] int idConnection)
        {
            var scheme = await _schemeService.GetScheme(id);

            if (scheme == null)
            {
                return NotFound();
            }

            var updatedScheme = await _schemeService.UpdateScheme(scheme,idConnection);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            string json = JsonSerializer.Serialize(updatedScheme, options);

            return Ok(json);
        }

        /// <summary>
        /// Удаляет схему по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы для удаления.</param>
        /// <returns>Результат удаления схемы.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheme(int id)
        {
            var scheme = await _schemeService.GetScheme(id);

            if (scheme == null)
            {
                return NotFound();
            }

            await _schemeService.DeleteScheme(id);

            return NoContent();
        }

        /// <summary>
        /// Получает схему по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <returns>Схема с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheme(int id)
        {
            var scheme = await _schemeService.GetScheme(id);

            if (scheme == null)
            {
                return NotFound();
            }

            return Ok(scheme);
        }

        /// <summary>
        /// Получает все схемы по заданному идентификатору книги.
        /// </summary>
        /// <param name="id">Идентификатор для получения всех схем книги.</param>
        /// <returns>Список всех схем для указанного идентификатора книги.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllScheme([FromBody] int id)
        {
            var schemes = await _schemeService.GetAllSchemes(id);

            if (schemes == null)
            {
                return NotFound();
            }

            return Ok(schemes);
        }
    }
}

