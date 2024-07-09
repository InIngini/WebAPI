using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User/Book/Scheme/[controller]")]
    public class ConnectionController : Controller
    {
        private readonly Context _context;

        public ConnectionController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateConnection([FromBody] ConnectionData connectionData)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Connection connection = new Connection()
            { 
                TypeConnection = connectionData.TypeConnection,
                IdCharacter1 = connectionData.IdCharacter1,
                IdCharacter2 = connectionData.IdCharacter2,
            };
            // Сохранение связи в базе данных
            _context.Connections.Add(connection);
            await _context.SaveChangesAsync();

            var scheme = await _context.Schemes
                .Where(s => s.NameScheme == "Главная схема" && s.IdBook == connectionData.IdBook)
                .FirstOrDefaultAsync();
            // Добавление связи в главную схему
            scheme.IdConnections.Add(connection);

            await _context.SaveChangesAsync();


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(connection, options);
            // Возврат созданной связи
            return CreatedAtAction(nameof(GetConnection), new { id = connection.IdConnection }, json);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnection(int id)
        {
            // Получение связи из базы данных
            var connection = await _context.Connections.FindAsync(id);

            // Если связь не найдена, вернуть ошибку
            if (connection == null)
            {
                return NotFound();
            }

            // Получение схем, связанных со связью, и удаление их оттуда
            // Получение всех схем
            var schemes = await _context.Schemes.ToListAsync();

            // Удаление `IdConnection` удаляемой связи из схем
            foreach (var scheme in schemes)
            {
                var connection1 = scheme.IdConnections.FirstOrDefault(c => c.IdConnection == id);
                if (connection1 != null)
                {
                    scheme.IdConnections.Remove(connection1);
                }
            }

            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();



            // Удаление связи
            _context.Connections.Remove(connection);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        //Получить связь
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConnection(int id)
        {
            var connection = await _context.Connections
                .Where(c => c.IdConnection == id)
                .Select(c => new
                {
                    c.TypeConnection,
                    c.IdCharacter1,
                    Character1 = c.IdCharacter1Navigation.Block1.Name,
                    c.IdCharacter2,
                    Character2 = c.IdCharacter2Navigation.Block1.Name,

                })
                .FirstOrDefaultAsync();

            if (connection == null)
            {
                return NotFound();
            }

            return Ok(connection);
        }
        //Получить все связи схемы
        [HttpGet("all")]
        public async Task<IActionResult> GetAllConnection([FromBody] int id)
        {
            var connections = await _context.Connections
                .Where(c => c.IdSchemes.Any(s => s.IdScheme == id))
                .Select(c => new
                {
                    c.TypeConnection,
                    c.IdCharacter1,
                    Character1 = c.IdCharacter1Navigation.Block1.Name,
                    c.IdCharacter2,
                    Character2 = c.IdCharacter2Navigation.Block1.Name,
                })
                .ToListAsync();

            if (connections == null)
            {
                return NotFound();
            }

            return Ok(connections);
        }

    }
}
