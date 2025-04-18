﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IGalleryService GalleryService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GalleryController"/>.
        /// </summary>
        /// <param name="galleryService">Сервис для работы с галереями.</param>
        public GalleryController(IGalleryService galleryService)
        {
            GalleryService = galleryService;
        }

        /// <summary>
        /// Создает новую галерею (то есть добавляет изображение к персонажу).
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///         "CharacterId": "1",
        ///         "PictureId": "1"
        ///     }
        ///
        /// </remarks>
        /// <param name="galleryData">Данные о галерее, которую необходимо создать.</param>
        /// <returns>Результат создания галереи.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BelongToGallery), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateGallery([FromBody] GalleryData galleryData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NotValidModel(ModelState));
            }
           
            var createdGallery = await GalleryService.CreateGallery(galleryData);

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
            await GalleryService.DeletePictureFromGallery(idPicture);

            return Ok();
        }

        /// <summary>
        /// Получает галерею (формально одно изображение) по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор галереи.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Галерея с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BelongToGallery), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGallery(int id, CancellationToken cancellationToken)
        {
            var gallery = await GalleryService.GetGallery(id, cancellationToken);

            if (gallery == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Изображение", 2));
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
        [ProducesResponseType(typeof(IEnumerable<BelongToGallery>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGallery([FromBody] int id, CancellationToken cancellationToken)
        {
            var galleries = await GalleryService.GetAllGalleries(id, cancellationToken);

            if (galleries == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Изображение", 2));
            }

            return Ok(galleries);
        }
    }
}
