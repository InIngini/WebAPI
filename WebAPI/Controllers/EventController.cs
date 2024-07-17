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
    //[Authorize]
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
            
            // Сохранение связи в базе данных
            var createdEvent = await _eventService.CreateEvent(eventData);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(createdEvent, options);
            // Возврат созданной связи
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.IdEvent }, json);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventData eventData)
        //{
        //    // Получение события из базы данных
        //    var @event = await _eventService.GetEvent(id);

        //    // Если событие не найдено, вернуть ошибку
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    var updatedEvent = await _eventService.UpdateEvent(eventData,id);

        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve
        //    };

        //    string json = JsonSerializer.Serialize(updatedEvent, options);
        //    // Возврат обновленного события
        //    return Ok(json);
        //}

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
