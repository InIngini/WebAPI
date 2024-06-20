using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryController : Controller
    {
        private readonly Context _context;

        public GalleryController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGallery([FromBody] GalleryData galleryData)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Gallery gallery=new Gallery()
            {
                IdCharacter = galleryData.IdCharacter,
                IdPicture = galleryData.IdPicture,
            };
            // Сохранение галереи в базе данных
            _context.Galleries.Add(gallery);
            await _context.SaveChangesAsync();

            // Возврат созданной галереи
            return CreatedAtAction(nameof(GetGallery), new { id = gallery.IdPicture }, gallery);
        }
        public class GalleryData
        {
            public int IdCharacter { get; set; }
            public int IdPicture { get; set; }
        }
        [HttpDelete("{idPicture}")]
        public async Task<IActionResult> DeletePictureFromGallery(int idPicture)
        {
            // Получение галереи из базы данных
            var gallery = _context.Galleries.FirstOrDefault(p => p.IdPicture == idPicture);

            // Если галерея не найдена, вернуть ошибку
            if (gallery == null)
            {
                return NotFound();
            }

            //Получение картинки из галереи
            var picture = _context.Pictures.FirstOrDefault(p => p.IdPicture == idPicture);

            // Если картинка не найдена, вернуть ошибку
            if (picture == null)
            {
                return NotFound();
            }
            
            // Удаление записи из галереи
            _context.Galleries.Remove(gallery);

            await _context.SaveChangesAsync();
            
            // Удаление картинки из галереи
            _context.Pictures.Remove(picture);

            await _context.SaveChangesAsync();

            

            // Возврат обновленной галереи
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGallery(int id)
        {
            var galery = await _context.Galleries.FindAsync(id);

            if (galery == null)
            {
                return NotFound();
            }

            return Ok(galery);
        }
    }
}
