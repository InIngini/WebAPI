using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления изображениями.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/[controller]")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PictureController"/>.
        /// </summary>
        /// <param name="pictureService">Сервис для работы с изображениями.</param>
        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        /// <summary>
        /// Создает новое изображение.
        /// </summary>
        /// <param name="picture">Данные о изображении, которое необходимо создать.</param>
        /// <returns>Результат создания изображения.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePicture([FromBody] Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPicture = await _pictureService.CreatePicture(picture);

            return CreatedAtAction(nameof(GetPicture), new { id = createdPicture.IdPicture }, createdPicture);
        }

        /// <summary>
        /// Удаляет изображение по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения для удаления.</param>
        /// <returns>Результат удаления изображения.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            var picture = await _pictureService.GetPicture(id);

            if (picture == null)
            {
                return NotFound();
            }

            await _pictureService.DeletePicture(id);

            return NoContent();
        }

        /// <summary>
        /// Получает изображение по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <returns>Изображение с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPicture(int id)
        {
            var picture = await _pictureService.GetPicture(id);

            if (picture == null)
            {
                return NotFound();
            }

            return Ok(picture);
        }
    }

}
