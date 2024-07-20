using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("User/Book/Scheme/[controller]")]
    public class ConnectionController : Controller
    {
        private readonly IConnectionService _connectionService;

        public ConnectionController(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConnection([FromBody] ConnectionData connectionData)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Сохранение связи в базе данных
            var createdConnection = await _connectionService.CreateConnection(connectionData);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(createdConnection, options);
            // Возврат созданной связи
            return CreatedAtAction(nameof(GetConnection), new { id = createdConnection.IdConnection }, json);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnection(int id)
        {
            // Получение связи из базы данных
            var connection = await _connectionService.DeleteConnection(id);

            // Если связь не найдена, вернуть ошибку
            if (connection == null)
            {
                return NotFound();
            }

            // Возврат подтверждения удаления
            return NoContent();
        }

        //Получить связь
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConnection(int id)
        {
            var connection = await _connectionService.GetConnection(id);

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
            var connections = await _connectionService.GetAllConnections(id);

            if (connections == null)
            {
                return NotFound();
            }
            

            return Ok(connections);
        }
    }

}
