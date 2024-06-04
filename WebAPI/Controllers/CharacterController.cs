using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : Controller
    {
        private readonly Context _context;

        public CharacterController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] Character character)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение персонажа в базе данных
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            // Возврат созданного персонажа
            return CreatedAtAction(nameof(GetCharacter), new { id = character.IdCharacter }, character);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            // Получение персонажа из базы данных
            var character = await _context.Characters.FindAsync(id);

            // Если персонаж не найден, вернуть ошибку
            if (character == null)
            {
                return NotFound();
            }

            // Удаление персонажа
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, [FromBody] Character character)
        {
            // Получение персонажа из базы данных
            var existingCharacter = await _context.Characters.FindAsync(id);

            // Если персонаж не найден, вернуть ошибку
            if (existingCharacter == null)
            {
                return NotFound();
            }

            // Обновление персонажа
            existingCharacter.IdBook = character.IdBook;
            existingCharacter.IdPicture = character.IdPicture;

            await _context.SaveChangesAsync();

            // Возврат обновленного персонажа
            return Ok(existingCharacter);
        }

    }
}
