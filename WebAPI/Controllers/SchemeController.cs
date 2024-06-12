using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemeController : Controller
    {
        private readonly Context _context;

        public SchemeController(Context context)
        {
            _context = context;
        }
        //Создание схемы
        [HttpPost]
        public async Task<IActionResult> CreateScheme([FromBody] Scheme scheme)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение схемы в базе данных
            _context.Schemes.Add(scheme);
            await _context.SaveChangesAsync();

            // Возврат созданной схемы
            return CreatedAtAction(nameof(GetScheme), new { id = scheme.IdScheme }, scheme);
        }
        //Изменение схемы
        [HttpPost]
        public async Task<IActionResult> AddScheme([FromBody] Scheme scheme)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение схемы в базе данных
            _context.Schemes.Add(scheme);
            await _context.SaveChangesAsync();

            // Возврат созданной схемы
            return CreatedAtAction(nameof(GetScheme), new { id = scheme.IdScheme }, scheme);
        }
        //Удаление схемы
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheme(int id)
        {
            // Получение схемы из базы данных
            var scheme = await _context.Schemes.FindAsync(id);

            // Если схема не найдена, вернуть ошибку
            if (scheme == null)
            {
                return NotFound();
            }

            // Удаление схемы
            _context.Schemes.Remove(scheme);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }

        //Получить схему
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheme(int id)
        {
            var scheme = await _context.Schemes.FindAsync(id);

            if (scheme == null)
            {
                return NotFound();
            }

            return Ok(scheme);
        }
    }
}
