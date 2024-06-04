using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private readonly Context _context;

        public EventController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event @event)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение события в базе данных
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            // Возврат созданного события
            return CreatedAtAction(nameof(GetEvent), new { id = @event.IdEvent }, @event);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event @event)
        {
            // Получение события из базы данных
            var existingEvent = await _context.Events.FindAsync(id);

            // Если событие не найдено, вернуть ошибку
            if (existingEvent == null)
            {
                return NotFound();
            }

            // Обновление события
            existingEvent.Name = @event.Name;
            existingEvent.Content = @event.Content;
            existingEvent.Time = @event.Time;

            await _context.SaveChangesAsync();

            // Возврат обновленного события
            return Ok(existingEvent);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            // Получение события из базы данных
            var @event = await _context.Events.FindAsync(id);

            // Если событие не найдено, вернуть ошибку
            if (@event == null)
            {
                return NotFound();
            }

            // Удаление события
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        [HttpDelete("{idBook}")]
        public async Task<IActionResult> DeleteAllEvents(int idBook)
        {
            // Получение списка событий книги
            var events = _context.Events.Where(e => e.IdBook == idBook);

            // Удаление всех событий
            _context.Events.RemoveRange(events);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        [HttpDelete("{idCharacter}")]
        public async Task<IActionResult> DeleteEventsForCharacter(int idCharacter)
        {
            // Получение списка событий персонажа
            var events = _context.Events.Where(e => e.IdCharacter == idCharacter);

            // Удаление всех событий
            _context.Events.RemoveRange(events);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
    }
}
