using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using WebAPI.Errors;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления изображениями в галерее персонажей.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/Book/Character/[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GalleryController"/>.
        /// </summary>
        /// <param name="galleryService">Сервис для работы с галереями.</param>
        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        /// <summary>
        /// Создает новую галерею (то есть добавляет изображение к персонажу).
        /// </summary>
        /// <param name="galleryData">Данные о галерее, которую необходимо создать.</param>
        /// <returns>Результат создания галереи.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateGallery([FromBody] GalleryData galleryData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NoValidModel(ModelState));
            }
           
            var createdGallery = await _galleryService.CreateGallery(galleryData);

            return CreatedAtAction(nameof(GetGallery), new { id = createdGallery.PictureId }, createdGallery);
        }

        /// <summary>
        /// Удаляет изображение из галереи.
        /// </summary>
        /// <param name="idPicture">Идентификатор изображения для удаления.</param>
        /// <returns>Результат удаления изображения.</returns>
        [HttpDelete("{idPicture}")]
        public async Task<IActionResult> DeletePictureFromGallery(int idPicture)
        {
            var gallery = await _galleryService.DeletePictureFromGallery(idPicture);

            if (gallery == null)
            {
                return NotFound(TypesOfErrors.NoFoundById("Изображение", 2));
            }

            return Ok();
        }

        /// <summary>
        /// Получает галерею (формально одно изображение) по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор галереи.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Галерея с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGallery(int id, CancellationToken cancellationToken)
        {
            var gallery = await _galleryService.GetGallery(id, cancellationToken);

            if (gallery == null)
            {
                return NotFound(TypesOfErrors.NoFoundById("Изображение", 2));
            }

            return Ok(gallery);
        }

        /// <summary>
        /// Получает все галереи (формально все изображения) по заданному идентификатору персонажа.
        /// </summary>
        /// <param name="id">Идентификатор персонажа, по которому ищутся все галереи.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех галерей для указанного идентификатора персонажа.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllGallery([FromBody] int id, CancellationToken cancellationToken)
        {
            var galleries = await _galleryService.GetAllGalleries(id, cancellationToken);

            if (galleries == null)
            {
                return NotFound(TypesOfErrors.NoFoundById("Изображение", 2));
            }

            return Ok(galleries);
        }
    }
}
