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
using WebAPI.Errors;

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
                var error = TypesOfErrors.NotValidModel(ModelState);
                return BadRequest(error);
            }
            var createdBook = await _bookService.CreateBook(bookdata);

            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }
        /// <summary>
        /// Обновляет существующую книгу.
        /// </summary>
        /// <param name="id">Идентификатор книги для обновления.</param>
        /// <param name="book">Новые данные о книге.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Результат выполнения операции обновления.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book,CancellationToken cancellationToken)
        {
            var existingBook = await _bookService.UpdateBook(id,book);

            if (existingBook == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Книга",0));
            }

            return Ok(existingBook);
        }

        /// <summary>
        /// Удаляет книгу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Результат выполнения операции удаления.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {

            await _bookService.DeleteBook(id);

            return Ok();
        }

        /// <summary>
        /// Получает книгу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Книга с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id,CancellationToken cancellationToken)
        {
            var book = await _bookService.GetBook(id,cancellationToken);

            if (book == null)
            {
                return NotFound(TypesOfErrors.NotFoundById("Книга", 0));
            }

            return Ok(book);
        }

        /// <summary>
        /// Получает все книги для заданного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список книг для указанного пользователя.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooksForUser([FromQuery] int userId, CancellationToken cancellationToken)
        {

            var books = await _bookService.GetAllBooksForUser(userId,cancellationToken);

            if (books == null || !books.Any())
            {
                books=new List<Book>();
            }

            return Ok(books);

        }
    }
}
