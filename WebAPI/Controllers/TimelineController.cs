//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace WebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class TimelineController : Controller
//    {
//        private readonly Context _context;

//        public TimelineController(Context context)
//        {
//            _context = context;
//        }
//        [HttpPost]
//        public async Task<IActionResult> CreateTimeline([FromBody] Timeline timeline)
//        {
//            // Проверка валидности модели
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Сохранение таймлайна в базе данных
//            _context.Timelines.Add(timeline);
//            await _context.SaveChangesAsync();

//            // Возврат созданного таймлайна
//            return CreatedAtAction(nameof(GetTimeline), new { id = timeline.IdTimeline }, timeline);
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddTimeline([FromBody] Timeline timeline)
//        {
//            // Проверка валидности модели
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Сохранение таймлайна в базе данных
//            _context.Timelines.Add(timeline);
//            await _context.SaveChangesAsync();

//            // Возврат созданного таймлайна
//            return CreatedAtAction(nameof(GetTimeline), new { id = timeline.IdTimeline }, timeline);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTimeline(int id)
//        {
//            // Получение таймлайна из базы данных
//            var timeline = await _context.Timelines.FindAsync(id);

//            // Если таймлайн не найден, вернуть ошибку
//            if (timeline == null)
//            {
//                return NotFound();
//            }

//            // Если таймлайн является главным, вернуть ошибку
//            if (timeline.IsMain)
//            {
//                return BadRequest("Нельзя удалить главный таймлайн");
//            }

//            // Удаление таймлайна
//            _context.Timelines.Remove(timeline);
//            await _context.SaveChangesAsync();

//            // Возврат подтверждения удаления
//            return NoContent();
//        }

//        [HttpDelete("{idBook}")]
//        public async Task<IActionResult> DeleteAllTimelines(int idBook)
//        {
//            // Получение списка таймлайнов книги
//            var timelines = _context.Timelines.Where(t => t.IdBook == idBook);

//            // Удаление всех таймлайнов
//            _context.Timelines.RemoveRange(timelines);
//            await _context.SaveChangesAsync();

//            // Возврат подтверждения удаления
//            return NoContent();
//        }


//    }
//}
