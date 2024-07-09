using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DAL.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User/Book/Timeline/[controller]")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventData eventData)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Event @event = new Event()
            {
                Name = eventData.Name,
                Content = eventData.Content,
                Time = eventData.Time,

            };
            if (eventData.IdCharacter != null)
            {
                foreach (var idCharacter in eventData.IdCharacter)
                {
                    var character = await _context.Characters.FindAsync(idCharacter);
                    if (character != null)
                    {
                        @event.IdCharacters.Add(character);
                    }
                }
            }
            // Сохранение связи в базе данных
            var createdEvent = await _eventService.CreateEvent(@event);

            var timeline = await _context.Timelines
                .Where(s => s.NameTimeline == "Главный таймлайн" && s.IdBook == eventData.IdBook)
                .FirstOrDefaultAsync();
            // Добавление связи в главную схему
            timeline.IdEvents.Add(createdEvent);

            await _context.SaveChangesAsync();


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(createdEvent, options);
            // Возврат созданной связи
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.IdEvent }, json);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventData eventData)
        {
            // Получение события из базы данных
            var @event = await _eventService.GetEvent(id);

            // Если событие не найдено, вернуть ошибку
            if (@event == null)
            {
                return NotFound();
            }

            // Обновление события
            @event.Name = eventData.Name;
            @event.Content = eventData.Content;
            @event.Time = eventData.Time;
            if (eventData.IdCharacter != null)
            {
                _context.Entry(@event).Collection(e => e.IdCharacters).Load();
                // Удаление существующих связей персонажей с событием
                foreach (var character in @event.IdCharacters.ToList())
                {
                    @event.IdCharacters.Remove(character);
                }

                foreach (var idCharacter in eventData.IdCharacter)
                {
                    var character = await _context.Characters.FindAsync(idCharacter);
                    if (character != null)
                    {
                        @event.IdCharacters.Add(character);
                    }
                }
            }

            var updatedEvent = await _eventService.UpdateEvent(@event);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(updatedEvent, options);
            // Возврат обновленного события
            return Ok(json);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            // Получение события из базы данных
            var @event = await _eventService.GetEvent(id);

            // Если событие не найдено, вернуть ошибку
            if (@event == null)
            {
                return NotFound();
            }

            await _eventService.DeleteEvent(id);

            // Возврат подтверждения удаления
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var @event = await _eventService.GetEvent(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEvents();

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }
    }
}
