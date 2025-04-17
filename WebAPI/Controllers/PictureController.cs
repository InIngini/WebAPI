using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using WebAPI.Errors;

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
        private readonly IPictureService PictureService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PictureController"/>.
        /// </summary>
        /// <param name="pictureService">Сервис для работы с изображениями.</param>
        public PictureController(IPictureService pictureService)
        {
            PictureService = pictureService;
        }

        /// <summary>
        /// Создает новое изображение.
        /// </summary>
        /// <param name="picture">Данные о изображении, которое необходимо создать.</param>
        /// <returns>Результат создания изображения.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Picture), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePicture([FromBody] Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NotValidModel(ModelState));
            }

            var createdPicture = await PictureService.CreatePicture(picture);

            return CreatedAtAction(nameof(GetPicture), new { id = createdPicture.Id }, createdPicture);
        }

        /// <summary>
        /// Удаляет изображение по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения для удаления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Результат удаления изображения.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id, CancellationToken cancellationToken)
        {
            await PictureService.DeletePicture(id);

            return Ok();
        }

        /// <summary>
        /// Получает изображение по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изображения.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Изображение с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Picture), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPicture(int id, CancellationToken cancellationToken)
        {
            var picture = await PictureService.GetPicture(id, cancellationToken);

            if (picture == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Изображение", 2));
            }

            return Ok(picture);
        }
    }

}
