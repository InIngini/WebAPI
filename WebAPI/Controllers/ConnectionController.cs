//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace WebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class ConnectionController : Controller
//    {
//        private readonly Context _context;

//        public ConnectionController(Context context)
//        {
//            _context = context;
//        }
//        [HttpPost]
//        public async Task<IActionResult> CreateConnection([FromBody] Connection connection)
//        {
//            // Проверка валидности модели
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Сохранение связи в базе данных
//            _context.Connections.Add(connection);
//            await _context.SaveChangesAsync();

//            // Возврат созданной связи
//            return CreatedAtAction(nameof(GetConnection), new { id = connection.IdConnection }, connection);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateConnection(int id, [FromBody] Connection connection)
//        {
//            // Получение связи из базы данных
//            var existingConnection = await _context.Connections.FindAsync(id);

//            // Если связь не найдена, вернуть ошибку
//            if (existingConnection == null)
//            {
//                return NotFound();
//            }

//            // Обновление связи
//            existingConnection.TypeConnection = connection.TypeConnection;
//            existingConnection.IdCharacter1 = connection.IdCharacter1;
//            existingConnection.IdCharacter2 = connection.IdCharacter2;

//            await _context.SaveChangesAsync();

//            // Возврат обновленной связи
//            return Ok(existingConnection);
//        }
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteConnection(int id)
//        {
//            // Получение связи из базы данных
//            var connection = await _context.Connections.FindAsync(id);

//            // Если связь не найдена, вернуть ошибку
//            if (connection == null)
//            {
//                return NotFound();
//            }

//            // Удаление связи
//            _context.Connections.Remove(connection);
//            await _context.SaveChangesAsync();
            
//            // Возврат подтверждения удаления
//            return NoContent();
//        }
//        [HttpDelete("{idBook}")]
//        public async Task<IActionResult> DeleteAllConnections(int idBook)
//        {
//            // Получение списка связей книги

//            //поменять на джоин

//            //var connections = _context.Connections.Where(c => c.IdBook == idBook);

//            //// Удаление всех связей
//            //_context.Connections.RemoveRange(connections);
//            //await _context.SaveChangesAsync();

//            // Возврат подтверждения удаления
//            return NoContent();
//        }

//        [HttpDelete("{idCharacter}")]
//        public async Task<IActionResult> DeleteConnectionsForCharacter(int idCharacter)
//        {
//            // Получение списка связей персонажа
//            var connections = _context.Connections.Where(c => c.IdCharacter1 == idCharacter || c.IdCharacter2 == idCharacter);

//            // Удаление всех связей
//            _context.Connections.RemoveRange(connections);
//            await _context.SaveChangesAsync();

//            // Возврат подтверждения удаления
//            return NoContent();
//        }


//    }
//}
