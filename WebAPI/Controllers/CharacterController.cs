using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Errors;
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
        private readonly ICharacterService CharacterService;
        private readonly IAddedAttributeService AddedAttributeService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CharacterController"/>.
        /// </summary>
        /// <param name="characterService">Сервис для работы с персонажами.</param>
        /// <param name="addedAttributeService">Сервис для работы с добавленными атрибутами.</param>
        public CharacterController(ICharacterService characterService, IAddedAttributeService addedAttributeService)
        {
            CharacterService = characterService;
            AddedAttributeService = addedAttributeService;
        }

        /// <summary>
        /// Создает нового персонажа и его ответов.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///          "BookId": 1
        ///     }
        ///     
        /// </remarks>
        /// <param name="bookCharacterData">Данные о персонаже, который необходимо создать.</param>
        /// <returns>Результат создания персонажа.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Character), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCharacter([FromBody] BookCharacterData bookCharacterData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NotValidModel(ModelState));
            }   

            Character character = new Character()
            {
                BookId = bookCharacterData.BookId,
                PictureId = bookCharacterData.PictureId
            };
            var createdCharacter = await CharacterService.CreateCharacter(character);

            return CreatedAtAction(nameof(GetCharacter), new { id = createdCharacter.Id }, createdCharacter);
        }

        /// <summary>
        /// Обновляет информацию о существующем персонаже и его ответах.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///          "Name": "Обновленное имя персонажа",
        ///          "Answers": [
        ///              "Обновленный ответ 1",
        ///              "Обновленный ответ 2",
        ///              "Обновленный ответ 3",
        ///              "Обновленный ответ 4",
        ///              "Обновленный ответ 5",
        ///              "Обновленный ответ 6",
        ///              "Обновленный ответ 1",
        ///              "Обновленный ответ 2",
        ///              "Обновленный ответ 3",
        ///              "Обновленный ответ 4",
        ///              "Обновленный ответ 5",
        ///              "Обновленный ответ 6",
        ///              "Обновленный ответ 7",
        ///              "Обновленный ответ 8",
        ///              "Обновленный ответ 9",
        ///              "Обновленный ответ 1",
        ///              "Обновленный ответ 2",
        ///              "Обновленный ответ 3",
        ///              "Обновленный ответ 4",
        ///              "Обновленный ответ 5",
        ///              "Обновленный ответ 6",
        ///              "Обновленный ответ 7",
        ///              "Обновленный ответ 8",
        ///              "Обновленный ответ 9",
        ///              "Обновленный ответ 1",
        ///              "Обновленный ответ 2",
        ///              "Обновленный ответ 3",
        ///              "Обновленный ответ 4",
        ///              "Обновленный ответ 5"
        ///          ]
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">Идентификатор персонажа для обновления.</param>
        /// <param name="characterWithAnswers">Новые данные о персонаже с ответами.</param>
        /// <returns>Результат обновления персонажа.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Character), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CharacterWithAnswers characterWithAnswers)
        {
            var existingCharacter = await CharacterService.UpdateCharacter(characterWithAnswers,id);

            if (existingCharacter == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Персонаж", 1));
            }

            return Ok(existingCharacter);
        }

        /// <summary>
        /// Удаляет персонажа по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор персонажа для удаления.</param>
        /// <returns>Результат удаления персонажа.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            await CharacterService.DeleteCharacter(id);

            return Ok();
        }

        /// <summary>
        /// Получает персонажа по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор персонажа.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Персонаж с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CharacterWithAnswers), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCharacter(int id, CancellationToken cancellationToken)
        {
            var character = await CharacterService.GetCharacter(id, cancellationToken);

            if (character == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Персонаж", 1));
            }

            return Ok(character);
        }

        /// <summary>
        /// Получает всех персонажей книги.
        /// </summary>
        /// <param name="idBook">Идентификатор книги.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список персонажей для указанной книги.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<CharacterAllData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCharacters([FromQuery] int idBook, CancellationToken cancellationToken)
        {
            var characters = await CharacterService.GetAllCharacters(idBook, cancellationToken);

            if (characters == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Персонажи", 3));
            }

            return Ok(characters);
        }

        ////////////////////////////Для вопросов//////////////////////////////

        /// <summary>
        /// Получает все вопросы.
        /// </summary>
        /// <returns>Список вопросов.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QuestionData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await CharacterService.GetQuestions();

            if (questions == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Вопросы", 3));
            }

            return Ok(questions);
        }

        ////////////////////////////Для добавленного атрибута//////////////////////////////

        /// <summary>
        /// Создает новый атрибут для персонажа.
        /// </summary>
        /// <remarks>
        /// Пример для использования: 
        /// 
        ///     {
        ///         "NumberAnswer": "1",
        ///         "NameAttribute": "Новое имя"
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">Идентификатор персонажа, к которому добавляется атрибут.</param>
        /// <param name="aa">Данные о добавляемом атрибуте.</param>
        /// <returns>Результат создания добавленного атрибута.</returns>
        [HttpPost("{id}/addedattribute")]
        [ProducesResponseType(typeof(AddedAttribute), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAddedAttribute(int id, [FromBody] AddedAttributeData aa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(TypesOfErrors.NotValidModel(ModelState));
            }

            var createdAddedAttribute = await AddedAttributeService.CreateAddedAttribute(id,aa);

            return CreatedAtAction(nameof(GetAddedAttribute), new { id = createdAddedAttribute.Id }, createdAddedAttribute);
        }

        /// <summary>
        /// Изменяет существующий добавленный атрибут.
        /// </summary>
        /// <param name="ida">Идентификатор добавленного атрибута для обновления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <param name="content">Обновленное содержимое атрибута.</param>
        /// <returns>Результат обновления добавленного атрибута.</returns>
        [HttpPut("{idc}/addedattribute/{ida}")]
        [ProducesResponseType(typeof(AddedAttribute), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAddedAttribute(int ida, [FromBody] string content, CancellationToken cancellationToken)
        {
            var existingAddedAttribute = await AddedAttributeService.UpdateAddedAttribute(ida,content);

            if (existingAddedAttribute == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Атрибут", 1));
            }

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
            await AddedAttributeService.DeleteAddedAttribute(idc, ida);

            return Ok();
        }

        /// <summary>
        /// Получает добавленный атрибут по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор добавленного атрибута.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Добавленный атрибут с указанным идентификатором.</returns>
        [HttpGet("addedattribute/{id}")]
        [ProducesResponseType(typeof(AddedAttribute), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAddedAttribute(int id, CancellationToken cancellationToken)
        {
            var addedAttribute = await AddedAttributeService.GetAddedAttribute(id,cancellationToken);

            if (addedAttribute == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Атрибут", 1));
            }

            return Ok(addedAttribute);
        }
    }
}
