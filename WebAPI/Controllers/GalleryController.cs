//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace WebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class GalleryController : Controller
//    {
//        private readonly Context _context;

//        public GalleryController(Context context)
//        {
//            _context = context;
//        }
//        [HttpPost]
//        public async Task<IActionResult> CreateGallery([FromBody] Gallery gallery)
//        {
//            // Проверка валидности модели
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Сохранение галереи в базе данных
//            _context.Galleries.Add(gallery);
//            await _context.SaveChangesAsync();

//            // Возврат созданной галереи
//            return CreatedAtAction(nameof(GetGallery), new { id = gallery.IdPicture }, gallery);
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddPictureToGallery(int idGallery, [FromBody] Picture picture)
//        {
//            // Получение галереи из базы данных
//            var gallery = await _context.Galleries.FindAsync(idGallery);

//            // Если галерея не найдена, вернуть ошибку
//            if (gallery == null)
//            {
//                return NotFound();
//            }

//            // Добавление картинки в галерею
//            //gallery.Pictures.Add(picture); //переделать

//            await _context.SaveChangesAsync();

//            // Возврат обновленной галереи
//            return Ok(gallery);
//        }
//        [HttpDelete("{idGallery}/{idPicture}")]
//        public async Task<IActionResult> DeletePictureFromGallery(int idGallery, int idPicture)
//        {
//            // Получение галереи из базы данных
//            var gallery = await _context.Galleries.FindAsync(idGallery);

//            // Если галерея не найдена, вернуть ошибку
//            if (gallery == null)
//            {
//                return NotFound();
//            }

//            // Получение картинки из галереи
//            //var picture = gallery.Pictures.FirstOrDefault(p => p.IdPicture == idPicture);

//            //// Если картинка не найдена, вернуть ошибку
//            //if (picture == null)
//            //{
//            //    return NotFound();
//            //}

//            //// Удаление картинки из галереи
//            //gallery.Pictures.Remove(picture);

//            await _context.SaveChangesAsync();

//            // Возврат обновленной галереи
//            return Ok(gallery);
//        }
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteGallery(int id)
//        {
//            // Получение галереи из базы данных
//            var gallery = await _context.Galleries.FindAsync(id);

//            // Если галерея не найдена, вернуть ошибку
//            if (gallery == null)
//            {
//                return NotFound();
//            }

//            // Удаление галереи
//            _context.Galleries.Remove(gallery);
//            await _context.SaveChangesAsync();

//            // Возврат подтверждения удаления
//            return NoContent();
//        }

//    }
//}
