using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DAL.Entities;
namespace WebAPI.Controllers
{
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
        public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CharacterWithBlocks characterWithBlocks)
        {
            // Получение персонажа из базы данных
            var existingCharacter = await _characterService.GetCharacter(id);

            // Если персонаж не найден, вернуть ошибку
            if (existingCharacter == null)
            {
                return NotFound();
            }

            // Обновление персонажа
            existingCharacter.IdPicture = characterWithBlocks.IdPicture;
            // Обновление блоков
            var block1 = await _context.Block1s.FindAsync(id);
            block1.Name = characterWithBlocks.Name;
            block1.Question1 = characterWithBlocks.Block1Question1;
            block1.Question2 = characterWithBlocks.Block1Question2;
            block1.Question3 = characterWithBlocks.Block1Question3;
            block1.Question4 = characterWithBlocks.Block1Question4;
            block1.Question5 = characterWithBlocks.Block1Question5;
            block1.Question6 = characterWithBlocks.Block1Question6;
            _context.Entry(block1).State = EntityState.Modified;
            var block2 = await _context.Block2s.FindAsync(id);
            block2.Question1 = characterWithBlocks.Block2Question1;
            block2.Question2 = characterWithBlocks.Block2Question2;
            block2.Question3 = characterWithBlocks.Block2Question3;
            block2.Question4 = characterWithBlocks.Block2Question4;
            block2.Question5 = characterWithBlocks.Block2Question5;
            block2.Question6 = characterWithBlocks.Block2Question6;
            block2.Question7 = characterWithBlocks.Block2Question7;
            block2.Question8 = characterWithBlocks.Block2Question8;
            block2.Question9 = characterWithBlocks.Block2Question9;
            _context.Entry(block2).State = EntityState.Modified;
            var block3 = await _context.Block3s.FindAsync(id);
            block3.Question1 = characterWithBlocks.Block3Question1;
            block3.Question2 = characterWithBlocks.Block3Question2;
            block3.Question3 = characterWithBlocks.Block3Question3;
            block3.Question4 = characterWithBlocks.Block3Question4;
            block3.Question5 = characterWithBlocks.Block3Question5;
            block3.Question6 = characterWithBlocks.Block3Question6;
            block3.Question7 = characterWithBlocks.Block3Question7;
            block3.Question8 = characterWithBlocks.Block3Question8;
            block3.Question9 = characterWithBlocks.Block3Question9;
            block3.Question10 = characterWithBlocks.Block3Question10;
            _context.Entry(block3).State = EntityState.Modified;
            var block4 = await _context.Block4s.FindAsync(id);
            block4.Question1 = characterWithBlocks.Block4Question1;
            block4.Question2 = characterWithBlocks.Block4Question2;
            block4.Question3 = characterWithBlocks.Block4Question3;
            block4.Question4 = characterWithBlocks.Block4Question4;
            block4.Question5 = characterWithBlocks.Block4Question5;
            _context.Entry(block4).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(existingCharacter, options);
            // Возврат обновленного персонажа в виде JSON
            return Ok(json);
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

            AddedAttribute addedAttribute = new AddedAttribute()
            {
                IdCharacter = id,
                NameAttribute = aa.NameAttribute,
                NumberBlock = aa.NumberBlock,
                ContentAttribute = String.Empty,
            };

            // Сохранение атрибута в базе данных
            var createdAddedAttribute = await _addedAttributeService.CreateAddedAttribute(addedAttribute);

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
        public async Task<IActionResult> DeleteAddedAttribute(int ida)
        {
            // Получение атрибута из базы данных
            var addedAttribute = await _addedAttributeService.DeleteAddedAttribute(ida);

            // Если атрибут не найден, вернуть ошибку
            if (addedAttribute == null)
            {
                return NotFound();
            }

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
