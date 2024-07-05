using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User/Book/Timeline/[controller]")]
    public class EventController : Controller
    {
        private readonly Context _context;

        public EventController(Context context)
        {
            _context = context;
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
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            var timeline = await _context.Timelines
                .Where(s => s.NameTimeline == "Главный таймлайн" && s.IdBook == eventData.IdBook)
                .FirstOrDefaultAsync();
            // Добавление связи в главную схему
            timeline.IdEvents.Add(@event);

            await _context.SaveChangesAsync();


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(@event, options);
            // Возврат созданной связи
            return CreatedAtAction(nameof(GetEvent), new { id = @event.IdEvent }, json);
        }
        public class EventData
        {
            public int? IdBook { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
            public string Time { get; set; }
            public int[]? IdCharacter {  get; set; }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventData eventData)
        {
            // Получение события из базы данных
            var @event = await _context.Events.FindAsync(id);

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

            await _context.SaveChangesAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(@event, options);
            // Возврат обновленного события
            return Ok(json);
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

            // Получение всех схем
            var timelines = await _context.Timelines.ToListAsync();

            // Удаление `IdConnection` удаляемой связи из схем
            foreach (var timeline in timelines)
            {
                var @event1 = timeline.IdEvents.FirstOrDefault(c => c.IdEvent == id);
                if (@event1 != null)
                {
                    timeline.IdEvents.Remove(@event1);
                }
            }

            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();

            // Удаление события
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var @event = await _context.Events
                .Where(c => c.IdEvent == id)
                .Select(c => new
                {
                    c.Name,
                    c.Time,
                    c.Content,
                    Characters = c.IdCharacters.Select(a => new
                    {
                        a.IdPicture,
                        a.IdCharacter,
                        a.Block1.Name
                    }),
                })
                .FirstOrDefaultAsync();
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEvent([FromBody] int id)
        {
            var @events = await _context.Events
                .Where(c => c.IdTimelines.Any(s => s.IdTimeline == id))
                .Select(c => new
                {
                    c.IdEvent,
                    c.Name,
                    c.Time
                })
                .ToListAsync();

            if (@events == null)
            {
                return NotFound();
            }

            return Ok(@events);
        }
    }
}
