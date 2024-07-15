using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DAL.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("User/Book/[controller]")]
    public class SchemeController : ControllerBase
    {
        private readonly ISchemeService _schemeService;

        public SchemeController(ISchemeService schemeService)
        {
            _schemeService = schemeService;
        }

        //Создание схемы
        [HttpPost]
        public async Task<IActionResult> CreateScheme([FromBody] SchemeData schemedata)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Scheme scheme = new Scheme()
            {
                IdBook = schemedata.IdBook,
                NameScheme = schemedata.NameScheme,
            };
            // Сохранение схемы в базе данных
            var createdScheme = await _schemeService.CreateScheme(scheme);

            // Возврат созданной схемы
            return CreatedAtAction(nameof(GetScheme), new { id = createdScheme.IdScheme }, createdScheme);

        }

        //Добавление связей в схему
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScheme(int id, [FromBody] int idConnection)
        {
            // Получение схемы из базы данных
            var scheme = await _schemeService.GetScheme(id);

            // Если схема не найдена, вернуть ошибку
            if (scheme == null)
            {
                return NotFound();
            }

            // Обновление схемы в базе данных
            var updatedScheme = await _schemeService.UpdateScheme(scheme,idConnection);
            

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(updatedScheme, options);

            // Возврат обновленной схемы
            return Ok(json);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheme(int id)
        {
            // Получение схемы из базы данных
            var scheme = await _schemeService.GetScheme(id);

            // Если схема не найдена, вернуть ошибку
            if (scheme == null)
            {
                return NotFound();
            }

            // Удаление схемы из базы данных
            await _schemeService.DeleteScheme(id);

            // Возврат подтверждения удаления
            return NoContent();
        }
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

