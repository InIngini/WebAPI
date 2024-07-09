using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DAL.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User/Book/Character/[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGallery([FromBody] GalleryData galleryData)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Gallery gallery = new Gallery()
            {
                IdCharacter = galleryData.IdCharacter,
                IdPicture = galleryData.IdPicture,
            };
            // Сохранение галереи в базе данных
            var createdGallery = await _galleryService.CreateGallery(gallery);

            // Возврат созданной галереи
            return CreatedAtAction(nameof(GetGallery), new { id = createdGallery.IdPicture }, createdGallery);
        }

        [HttpDelete("{idPicture}")]
        public async Task<IActionResult> DeletePictureFromGallery(int idPicture)
        {
            // Получение галереи из базы данных
            var gallery = await _galleryService.DeletePictureFromGallery(idPicture);

            // Если галерея не найдена, вернуть ошибку
            if (gallery == null)
            {
                return NotFound();
            }

            // Возврат обновленной галереи
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGallery(int id)
        {
            var gallery = await _galleryService.GetGallery(id);

            if (gallery == null)
            {
                return NotFound();
            }

            return Ok(gallery);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGallery([FromBody] int id)
        {
            var galleries = await _galleryService.GetAllGalleries(id);

            if (galleries == null)
            {
                return NotFound();
            }

            return Ok(galleries);
        }
    }
}
