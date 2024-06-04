using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly Context _context;

        public BookController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение книги в базе данных
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Возврат созданной книги
            return CreatedAtAction(nameof(GetBook), new { id = book.IdBook }, book);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            // Получение книги из базы данных
            var existingBook = await _context.Books.FindAsync(id);

            // Если книга не найдена, вернуть ошибку
            if (existingBook == null)
            {
                return NotFound();
            }

            // Обновление книги
            existingBook.NameBook = book.NameBook;
            existingBook.IdPicture = book.IdPicture;

            await _context.SaveChangesAsync();

            // Возврат обновленной книги
            return Ok(existingBook);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            // Получение книги из базы данных
            var book = await _context.Books.FindAsync(id);

            // Если книга не найдена, вернуть ошибку
            if (book == null)
            {
                return NotFound();
            }

            // Удаление книги
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }

    }
}
