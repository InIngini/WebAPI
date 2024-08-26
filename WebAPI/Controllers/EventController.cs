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
    /// <summary>
    /// Контроллер для управления событиями на временной шкале.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/Book/Timeline/[controller]")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventController"/>.
        /// </summary>
        /// <param name="eventService">Сервис для работы с событиями.</param>
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Создает новое событие.
        /// </summary>
        /// <param name="eventData">Данные о событии, которое необходимо создать.</param>
        /// <returns>Результат создания события.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventData eventData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdEvent = await _eventService.CreateEvent(eventData);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            string json = JsonSerializer.Serialize(createdEvent, options);

            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, json);
        }

        /// <summary>
        /// Обновляет существующее событие.
        /// </summary>
        /// <param name="id">Идентификатор события для обновления.</param>
        /// <param name="eventData">Обновленные данные о событии.</param>
        /// <returns>Результат обновления события.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventData eventData)
        {
            var @event = await _eventService.GetEvent(id);

            if (@event == null)
            {
                return NotFound();
            }

            var updatedEvent = await _eventService.UpdateEvent(eventData, id);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            string json = JsonSerializer.Serialize(updatedEvent, options);
            
            return Ok(json);
        }

        /// <summary>
        /// Удаляет событие по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор события для удаления.</param>
        /// <returns>Результат удаления события.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _eventService.GetEvent(id);

            if (@event == null)
            {
                return NotFound();
            }
            await _eventService.DeleteEvent(id);

            return NoContent();
        }

        /// <summary>
        /// Получает событие по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор события.</param>
        /// <returns>Событие с указанным идентификатором.</returns>
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

        /// <summary>
        /// Получает все события таймлайна по идентификатору таймлайна.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна, по которому ищутся все события.</param>
        /// <returns>Список всех событий для указанного идентификатора таймлайна.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEvents([FromBody] int id)
        {
            var events = await _eventService.GetAllEvents(id);

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }
    }
}
