using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("User/Book/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineService _timelineService;

        public TimelineController(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        //Создание схемы
        [HttpPost]
        public async Task<IActionResult> CreateTimeline([FromBody] TimelineData timelinedata)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Timeline timeline = new Timeline()
            {
                IdBook = timelinedata.IdBook,
                NameTimeline = timelinedata.NameTimeline,
            };
            // Сохранение схемы в базе данных
            var createdTimeline = await _timelineService.CreateTimeline(timeline);

            // Возврат созданной схемы
            return CreatedAtAction(nameof(GetTimeline), new { id = createdTimeline.IdTimeline }, createdTimeline);
        }

        //Добавление связей в схему
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeline(int id, [FromBody] int idEvent)
        {
            
            // Получение таймлайна из базы данных
            var timeline = await _timelineService.GetTimeline(id);

            // Если таймлайн не найден, вернуть ошибку
            if (timeline == null)
            {
                return NotFound();
            }
            
            // Обновление таймлайна в базе данных
            var updatedTimeline = await _timelineService.UpdateTimeline(timeline, idEvent);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(updatedTimeline, options);

            // Возврат обновленного таймлайна
            return Ok(json);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeline(int id)
        {
            // Получение таймлайна из базы данных
            var timeline = await _timelineService.GetTimeline(id);

            // Если таймлайн не найден, вернуть ошибку
            if (timeline == null)
            {
                return NotFound();
            }

            // Удаление таймлайна из базы данных
            await _timelineService.DeleteTimeline(id);

            // Возврат подтверждения удаления
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeline(int id)
        {
            var timeline = await _timelineService.GetTimeline(id);

            if (timeline == null)
            {
                return NotFound();
            }

            return Ok(timeline);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTimeline([FromBody] int id)
        {
            var timelines = await _timelineService.GetAllTimelines(id);

            if (timelines == null)
            {
                return NotFound();
            }

            return Ok(timelines);
        }
    }
}

