﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User/Book/[controller]")]
    public class TimelineController : Controller
    {
        private readonly Context _context;

        public TimelineController(Context context)
        {
            _context = context;
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
            _context.Timelines.Add(timeline);
            await _context.SaveChangesAsync();

            // Возврат созданной схемы
            return CreatedAtAction(nameof(GetTimeline), new { id = timeline.IdTimeline }, timeline);

        }

        //Добавление связей в схему
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeline(int id, [FromBody] int idEvent)
        {
            // Получение книги из базы данных
            var timeline = await _context.Timelines.FindAsync(id);

            // Если книга не найдена, вернуть ошибку
            if (timeline == null)
            {
                return NotFound();
            }

            // Поиск соединения по указанному идентификатору
            var @event = await _context.Events.FindAsync(idEvent);

            // Если соединение не найдено, вернуть ошибку
            if (@event == null)
            {
                return NotFound();
            }

            // Добавление соединения в схему
            timeline.IdEvents.Add(@event);

            await _context.SaveChangesAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(timeline, options);

            // Возврат обновленной книги
            return Ok(json);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeline(int id)
        {
            // Получение таймлайна из базы данных
            var timeline = await _context.Timelines.FindAsync(id);

            // Если таймлайн не найден, вернуть ошибку
            if (timeline == null)
            {
                return NotFound();
            }

            // Удаление таймлайна
            _context.Timelines.Remove(timeline);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeline(int id)
        {
            var timeline = await _context.Timelines
                .Where(c => c.IdBook == id)
                .Select(c => new
                {
                    c.IdTimeline,
                    c.NameTimeline
                })
                .FirstOrDefaultAsync();

            if (timeline == null)
            {
                return NotFound();
            }

            return Ok(timeline);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTimeline([FromBody] int id)
        {
            var timelines = _context.Timelines
                .Where(c => c.IdBook == id)
                .Select(c => new
                {
                    c.IdTimeline,
                    c.NameTimeline
                })
                .AsEnumerable();

            if (timelines == null)
            {
                return NotFound();
            }

            return Ok(timelines);
        }
    }
}
