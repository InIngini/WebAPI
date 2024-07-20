using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("User/[controller]")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
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
            var createdPicture = await _pictureService.CreatePicture(picture);

            // Возврат созданной картинки
            return CreatedAtAction(nameof(GetPicture), new { id = createdPicture.IdPicture }, createdPicture);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            // Получение картинки из базы данных
            var picture = await _pictureService.GetPicture(id);

            // Если картинка не найдена, вернуть ошибку
            if (picture == null)
            {
                return NotFound();
            }

            // Удаление картинки
            await _pictureService.DeletePicture(id);

            // Возврат подтверждения удаления
            return NoContent();
        }

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
