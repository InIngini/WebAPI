using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        //Создание книги
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] UserBookData bookdata)
        {
            // Создание сущности книги
            Book book = new Book
            {
                NameBook = bookdata.NameBook,
                IdPicture = bookdata.IdPicture
            };

            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Сохранение книги в базе данных
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Создание сущности BelongTo
            BelongToBook belonToBook = new BelongToBook
            {
                IdUser = bookdata.idUser,
                IdBook = book.IdBook,
                TypeBelong = 0 // Установить значение по умолчанию для статуса
            };

            // Сохранение BelongTo в базе данных
            _context.BelongToBooks.Add(belonToBook);
            await _context.SaveChangesAsync();

            // Создание главной схемы 
            Scheme scheme = new Scheme()
            {
                NameScheme = "Главная схема",
                IdBook = book.IdBook,
            };
            _context.Schemes.Add(scheme);
            await _context.SaveChangesAsync();

            // Создание главного таймлайна
            Timeline timeline = new Timeline()
            {
                NameTimeline = "Главный таймлайн",
                IdBook = book.IdBook,
            };
            _context.Timelines.Add(timeline);
            await _context.SaveChangesAsync();

            // Настройка параметров сериализации
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            // Сериализация созданной книги в JSON
            string json = JsonSerializer.Serialize(book, options);

            // Возврат созданной книги в формате JSON
            return CreatedAtAction(nameof(GetBook), new { id = book.IdBook }, json);
        }

        //вспомогательный тип
        public class UserBookData
        {
            public int idUser { get; set; }
            public string NameBook { get; set; }
            public int? IdPicture { get; set; }
        }
        //Изменение книги
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
        //Удаление книги
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

            // Удаление связанных записей из таблицы BelongToBook
            var belongToBookRecords = _context.BelongToBooks.Where(b => b.IdBook == id);
            _context.BelongToBooks.RemoveRange(belongToBookRecords);
            await _context.SaveChangesAsync();

            //Удаление всех схем


            //Удаление всех таймлайнов


            //Удаление всех персонажей, блоков и добавленных атрибутов



            //Удаление всех изображений



            // Удаление книги
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        //Получить книгу
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

    }
}
