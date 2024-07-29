using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;
namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("User/Book/[controller]")]
    public class CharacterController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly IAddedAttributeService _addedAttributeService;

        public CharacterController(ICharacterService characterService, IAddedAttributeService addedAttributeService)
        {
            _characterService = characterService;
            _addedAttributeService = addedAttributeService;
        }

        //Создание персонажа и блоков
        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] BookCharacterData bookCharacterData)
        {
            Character character = new Character()
            {
                IdBook = bookCharacterData.IdBook,
                IdPicture = bookCharacterData.IdPicture
            };

            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var createdCharacter = await _characterService.CreateCharacter(character);


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(createdCharacter, options);

            // Возврат созданного персонажа в виде JSON
            return CreatedAtAction(nameof(GetCharacter), new { id = createdCharacter.IdCharacter }, json);
        }

        // Изменение персонажа и блоков
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CharacterWithAnswers characterWithAnswers)
        {
            var existingCharacter = _characterService.UpdateCharacter(characterWithAnswers,id);

            // Если персонаж не найден, вернуть ошибку
            if (existingCharacter == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(existingCharacter, options);
            // Возврат обновленного персонажа в виде JSON
            return Ok(json);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            await _characterService.DeleteCharacter(id);

            return Ok();
        }
        // Получение персонажа по его идентификатору
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var character = await _characterService.GetCharacter(id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // Получение всех персонажей книги
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCharacters([FromBody] int idBook)
        {
            var characters = await _characterService.GetAllCharacters(idBook);

            if (characters == null)
            {
                return NotFound();
            }

            return Ok(characters);
        }

        [HttpGet]//получить вопросы
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _characterService.GetQuestions();

            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }
        ////////////////////////////Для добавленного атрибута//////////////////////////////

        /////Создать атрибут
        [HttpPost("{id}/addedattribute")]
        public async Task<IActionResult> CreateAddedAttribute(int id, [FromBody] AAData aa)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            // Сохранение атрибута в базе данных
            var createdAddedAttribute = await _addedAttributeService.CreateAddedAttribute(id,aa);

            // Возврат созданного атрибута
            return CreatedAtAction(nameof(GetAddedAttribute), new { id = createdAddedAttribute.IdAttribute }, createdAddedAttribute);
        }

        //Изменить атрибут
        [HttpPut("{idc}/addedattribute/{ida}")]
        public async Task<IActionResult> UpdateAddedAttribute(int ida, [FromBody] string content)
        {
            // Получение атрибута из базы данных
            var existingAddedAttribute = await _addedAttributeService.GetAddedAttribute(ida);
            // Если атрибут не найден, вернуть ошибку
            if (existingAddedAttribute == null)
            {
                return NotFound();
            }

            // Обновление атрибута
            existingAddedAttribute.ContentAttribute = content;

            await _addedAttributeService.UpdateAddedAttribute(existingAddedAttribute);

            // Возврат обновленного атрибута
            return Ok(existingAddedAttribute);
        }

        //Удалить атрибут
        [HttpDelete("{idc}/addedattribute/{ida}")]
        public async Task<IActionResult> DeleteAddedAttribute(int idc,int ida)
        {
            // Получение атрибута из базы данных
            await _addedAttributeService.DeleteAddedAttribute(idc, ida);

            // Возврат подтверждения удаления
            return NoContent();
        }

        //Получить атрибут
        [HttpGet("addedattribute/{id}")]
        public async Task<IActionResult> GetAddedAttribute(int id)
        {
            var addedAttribute = await _addedAttributeService.GetAddedAttribute(id);

            if (addedAttribute == null)
            {
                return NotFound();
            }

            return Ok(addedAttribute);
        }
    }
}
