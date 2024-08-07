﻿using Microsoft.AspNetCore.Http;
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
    /// <summary>
    /// Контроллер для управления персонажами и атрибутами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/Book/[controller]")]
    public class CharacterController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly IAddedAttributeService _addedAttributeService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CharacterController"/>.
        /// </summary>
        /// <param name="characterService">Сервис для работы с персонажами.</param>
        /// <param name="addedAttributeService">Сервис для работы с добавленными атрибутами.</param>
        public CharacterController(ICharacterService characterService, IAddedAttributeService addedAttributeService)
        {
            _characterService = characterService;
            _addedAttributeService = addedAttributeService;
        }

        /// <summary>
        /// Создает нового персонажа и его ответов.
        /// </summary>
        /// <param name="bookCharacterData">Данные о персонаже, который необходимо создать.</param>
        /// <returns>Результат создания персонажа.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] BookCharacterData bookCharacterData)
        {
            Character character = new Character()
            {
                IdBook = bookCharacterData.IdBook,
                IdPicture = bookCharacterData.IdPicture
            };

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

            return CreatedAtAction(nameof(GetCharacter), new { id = createdCharacter.IdCharacter }, json);
        }

        /// <summary>
        /// Обновляет информацию о существующем персонаже и его ответах.
        /// </summary>
        /// <param name="id">Идентификатор персонажа для обновления.</param>
        /// <param name="characterWithAnswers">Новые данные о персонаже с ответами.</param>
        /// <returns>Результат обновления персонажа.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CharacterWithAnswers characterWithAnswers)
        {
            var existingCharacter = _characterService.UpdateCharacter(characterWithAnswers,id);

            if (existingCharacter == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            string json = JsonSerializer.Serialize(existingCharacter, options);
            
            return Ok(json);
        }
        /// <summary>
        /// Удаляет персонажа по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор персонажа для удаления.</param>
        /// <returns>Результат удаления персонажа.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            await _characterService.DeleteCharacter(id);

            return Ok();
        }
        /// <summary>
        /// Получает персонажа по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор персонажа.</param>
        /// <returns>Персонаж с указанным идентификатором.</returns>
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

        /// <summary>
        /// Получает всех персонажей книги.
        /// </summary>
        /// <param name="idBook">Идентификатор книги.</param>
        /// <returns>Список персонажей для указанной книги.</returns>
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

        ////////////////////////////Для вопросов//////////////////////////////

        /// <summary>
        /// Получает все вопросы.
        /// </summary>
        /// <returns>Список вопросов.</returns>
        [HttpGet]
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

        /// <summary>
        /// Создает новый атрибут для персонажа.
        /// </summary>
        /// <param name="id">Идентификатор персонажа, к которому добавляется атрибут.</param>
        /// <param name="aa">Данные о добавляемом атрибуте.</param>
        /// <returns>Результат создания добавленного атрибута.</returns>
        [HttpPost("{id}/addedattribute")]
        public async Task<IActionResult> CreateAddedAttribute(int id, [FromBody] AAData aa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAddedAttribute = await _addedAttributeService.CreateAddedAttribute(id,aa);

            return CreatedAtAction(nameof(GetAddedAttribute), new { id = createdAddedAttribute.IdAttribute }, createdAddedAttribute);
        }

        /// <summary>
        /// Изменяет существующий добавленный атрибут.
        /// </summary>
        /// <param name="ida">Идентификатор добавленного атрибута для обновления.</param>
        /// <param name="content">Обновленное содержимое атрибута.</param>
        /// <returns>Результат обновления добавленного атрибута.</returns>
        [HttpPut("{idc}/addedattribute/{ida}")]
        public async Task<IActionResult> UpdateAddedAttribute(int ida, [FromBody] string content)
        {
            var existingAddedAttribute = await _addedAttributeService.GetAddedAttribute(ida);
            if (existingAddedAttribute == null)
            {
                return NotFound();
            }

            existingAddedAttribute.ContentAttribute = content;
            await _addedAttributeService.UpdateAddedAttribute(existingAddedAttribute);

            return Ok(existingAddedAttribute);
        }

        /// <summary>
        /// Удаляет добавленный атрибут по его идентификатору.
        /// </summary>
        /// <param name="idc">Идентификатор персонажа, к которому относится атрибут.</param>
        /// <param name="ida">Идентификатор добавленного атрибута для удаления.</param>
        /// <returns>Результат выполнения операции удаления.</returns>
        [HttpDelete("{idc}/addedattribute/{ida}")]
        public async Task<IActionResult> DeleteAddedAttribute(int idc,int ida)
        {
            await _addedAttributeService.DeleteAddedAttribute(idc, ida);

            return NoContent();
        }

        /// <summary>
        /// Получает добавленный атрибут по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор добавленного атрибута.</param>
        /// <returns>Добавленный атрибут с указанным идентификатором.</returns>
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
