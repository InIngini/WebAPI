using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

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
            _context.Schemes.Add(scheme);
            await _context.SaveChangesAsync();

            // Возврат созданной схемы
            return CreatedAtAction(nameof(GetScheme), new { id = scheme.IdScheme }, scheme);

        }
        //вспомогательный тип
        public class SchemeData
        {
            public int IdBook { get; set; }
            public string NameScheme { get; set; }
        }
        //Добавление связей в схему
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] int idConnection)
        {
            // Получение книги из базы данных
            var scheme = await _context.Schemes.FindAsync(id);

            // Если книга не найдена, вернуть ошибку
            if (scheme == null)
            {
                return NotFound();
            }

            // Поиск соединения по указанному идентификатору
            var connection = await _context.Connections.FindAsync(idConnection);

            // Если соединение не найдено, вернуть ошибку
            if (connection == null)
            {
                return NotFound();
            }

            // Добавление соединения в схему
            scheme.IdConnections.Add(connection);

            await _context.SaveChangesAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(scheme, options);

            // Возврат обновленной книги
            return Ok(json);
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
