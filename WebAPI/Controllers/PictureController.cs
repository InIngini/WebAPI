using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User/[controller]")]
    public class PictureController : Controller
    {
        private readonly Context _context;

        public PictureController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePicture([FromBody] Picture picture)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение картинки в базе данных
            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();

            // Возврат созданной картинки
            return CreatedAtAction(nameof(GetPicture), new { id = picture.IdPicture }, picture);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            // Получение картинки из базы данных
            var picture = await _context.Pictures.FindAsync(id);

            // Если картинка не найдена, вернуть ошибку
            if (picture == null)
            {
                return NotFound();
            }

            // Удаление картинки
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();



            // Возврат подтверждения удаления
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPicture(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);

            if (picture == null)
            {
                return NotFound();
            }

            return Ok(picture);
        }
    }
}
