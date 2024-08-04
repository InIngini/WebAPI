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
    /// <summary>
    /// Контроллер для управления таймлайнами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/Book/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineService _timelineService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TimelineController"/>.
        /// </summary>
        /// <param name="timelineService">Сервис для работы с таймлайнами.</param>
        public TimelineController(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        /// <summary>
        /// Создает новый таймлайн.
        /// </summary>
        /// <param name="timelinedata">Данные о таймлайне, который необходимо создать.</param>
        /// <returns>Результат создания таймлайна.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTimeline([FromBody] TimelineData timelinedata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTimeline = await _timelineService.CreateTimeline(timelinedata);
            return CreatedAtAction(nameof(GetTimeline), new { id = createdTimeline.IdTimeline }, createdTimeline);
        }

        /// <summary>
        /// Обновляет таймлайн, добавляя событие.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна.</param>
        /// <param name="idEvent">Идентификатор события для добавления.</param>
        /// <returns>Обновлённый таймлайн.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeline(int id, [FromBody] int idEvent)
        {
            var timeline = await _timelineService.GetTimeline(id);
            if (timeline == null)
            {
                return NotFound();
            }

            var updatedTimeline = await _timelineService.UpdateTimeline(timeline, idEvent);
            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve };
            string json = JsonSerializer.Serialize(updatedTimeline, options);
            return Ok(json);
        }

        /// <summary>
        /// Удаляет таймлайн по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна для удаления.</param>
        /// <returns>Результат удаления таймлайна.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeline(int id)
        {
            var timeline = await _timelineService.GetTimeline(id);
            if (timeline == null)
            {
                return NotFound();
            }

            await _timelineService.DeleteTimeline(id);
            return NoContent();
        }
        /// <summary>
        /// Получает таймлайн по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор таймлайна.</param>
        /// <returns>Таймлайн с указанным идентификатором.</returns>
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

        /// <summary>
        /// Получает все таймлайны по заданному идентификатору книги.
        /// </summary>
        /// <param name="id">Идентификатор книги для получения всех таймлайнов.</param>
        /// <returns>Список всех таймлайнов для указанного идентификатора книги.</returns>
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

