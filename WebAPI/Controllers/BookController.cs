using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPI.BLL.DTO;
using WebAPI.BLL.Interfaces;
using WebAPI.DAL.Entities;
using WebAPI.BLL.Token;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("User/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ITokenValidator _tokenValidator;

        public BookController(IBookService bookService, ITokenValidator tokenValidator)
        {
            _bookService = bookService;
            _tokenValidator = tokenValidator;
        }

        //Создание книги
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] UserBookData bookdata)
        {

            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение книги в базе данных
            var createdBook = await _bookService.CreateBook(bookdata);

            // Настройка параметров сериализации
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            // Сериализация созданной книги в JSON
            string json = JsonSerializer.Serialize(createdBook, options);

            // Возврат созданной книги в формате JSON
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.IdBook }, json);
        }

        //Изменение книги
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            // Получение книги из базы данных
            var existingBook = await _bookService.GetBook(id);

            // Если книга не найдена, вернуть ошибку
            if (existingBook == null)
            {
                return NotFound();
            }

            // Обновление книги
            existingBook.NameBook = book.NameBook;
            existingBook.IdPicture = book.IdPicture;

            await _bookService.UpdateBook(existingBook);

            // Возврат обновленной книги
            return Ok(existingBook);
        }

        //Удаление книги
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            // Получение книги из базы данных
            var book = await _bookService.GetBook(id);

            // Если книга не найдена, вернуть ошибку
            if (book == null)
            {
                return NotFound();
            }

            await _bookService.DeleteBook(id);

            // Возврат подтверждения удаления
            return NoContent();
        }

        //Получение книги
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

        //Получение всех книг для пользователя
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooksForUser([FromBody] string token)
        {

            var userId = _tokenValidator.ValidateToken(token);

            if (userId > 0)
            {
                var books = await _bookService.GetAllBooksForUser(userId);

                if (books == null || !books.Any())
                {
                    books=new List<Book>();
                }

                return Ok(books);

            }
            else
            {
                return Unauthorized("Invalid token");
            }
        }
    }
}
