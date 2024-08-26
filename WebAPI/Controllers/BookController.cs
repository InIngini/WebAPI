using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DB.Entities;
using WebAPI.BLL.Token;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления книгами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("User/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ITokenValidator _tokenValidator;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookController"/>.
        /// </summary>
        /// <param name="bookService">Сервис для работы с книгами.</param>
        /// <param name="tokenValidator">Сервис для валидации токенов.</param>
        public BookController(IBookService bookService, ITokenValidator tokenValidator)
        {
            _bookService = bookService;
            _tokenValidator = tokenValidator;
        }

        /// <summary>
        /// Создает новую книгу.
        /// </summary>
        /// <param name="bookdata">Данные о книге, которые необходимо создать.</param>
        /// <returns>Результат создания книги.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] UserBookData bookdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdBook = await _bookService.CreateBook(bookdata);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            string json = JsonSerializer.Serialize(createdBook, options);

            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, json);
        }
        /// <summary>
        /// Обновляет существующую книгу.
        /// </summary>
        /// <param name="id">Идентификатор книги для обновления.</param>
        /// <param name="book">Новые данные о книге.</param>
        /// <returns>Результат выполнения операции обновления.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            var existingBook = await _bookService.GetBook(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            // Обновление книги
            existingBook.NameBook = book.NameBook;
            existingBook.PictureId = book.PictureId;
            await _bookService.UpdateBook(existingBook);

            return Ok(existingBook);
        }

        /// <summary>
        /// Удаляет книгу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления.</param>
        /// <returns>Результат выполнения операции удаления.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookService.DeleteBook(id);

            return NoContent();
        }

        /// <summary>
        /// Получает книгу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Книга с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// Получает все книги для заданного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список книг для указанного пользователя.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooksForUser([FromBody] int userId)
        {

            var books = await _bookService.GetAllBooksForUser(userId);

            if (books == null || !books.Any())
            {
                books=new List<Book>();
            }

            return Ok(books);

        }
    }
}
