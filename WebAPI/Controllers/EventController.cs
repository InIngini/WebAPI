using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Errors;
using Microsoft.Extensions.Logging;

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
        private readonly IEventService EventService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventController"/>.
        /// </summary>
        /// <param name="eventService">Сервис для работы с событиями.</param>
        public EventController(IEventService eventService)
        {
            EventService = eventService;
        }

        /// <summary>
        /// Создает новое событие.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        ///
        ///     {
        ///         "BookId": "1",
        ///         "Name": "Новое имя",
        ///         "Content": "Какой-то контент",
        ///         "Time": "10.06.2024-20.06.2024",
        ///         "CharactersId": [1,2]
        ///     }
        ///
        /// </remarks>
        /// <param name="eventData">Данные о событии, которое необходимо создать.</param>
        /// <returns>Результат создания события.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Event), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEvent([FromBody] EventData eventData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NotValidModel(ModelState));
            }
            
            var createdEvent = await EventService.CreateEvent(eventData);

            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
        }

        /// <summary>
        /// Обновляет существующее событие.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        ///
        ///     {
        ///         "Name": "Новое имя",
        ///         "Content": "Какой-то",
        ///         "Time": "10.06.2024-20.06.2024",
        ///         "CharactersId": [1]
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Идентификатор события для обновления.</param>
        /// <param name="eventData">Обновленные данные о событии.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Результат обновления события.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventData eventData, CancellationToken cancellationToken)
        {
            var updatedEvent = await EventService.UpdateEvent(eventData, id);

            if (updatedEvent == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Событие", 2));
            }

            return Ok(updatedEvent);
        }

        /// <summary>
        /// Удаляет событие по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор события для удаления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Результат удаления события.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id,CancellationToken cancellationToken)
        {
            await EventService.DeleteEvent(id);

            return Ok();
        }

        /// <summary>
        /// Получает событие по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор события.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Событие с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventData), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEvent(int id, CancellationToken cancellationToken)
        {
            var @event = await EventService.GetEvent(id, cancellationToken);

            if (@event == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Событие", 2));
            }

            return Ok(@event);
        }

        /// <summary>
        /// Получает все события таймлайна по идентификатору таймлайна.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна, по которому ищутся все события.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех событий для указанного идентификатора таймлайна.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<EventAllData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEvents([FromBody] int id, CancellationToken cancellationToken)
        {
            var events = await EventService.GetAllEvents(id, cancellationToken);

            if (events == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("События", 3));
            }

            return Ok(events);
        }
    }
}
