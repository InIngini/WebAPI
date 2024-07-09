using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPI.Token;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User/[controller]")]
    public class BookController : Controller
    {
        private readonly Context _context;
        private readonly ITokenValidator _tokenValidator;

        public BookController(Context context, ITokenValidator tokenValidator)
        {
            _context = context;
            _tokenValidator = tokenValidator;
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
            // Получение главной схемы
            var mainScheme = _context.Schemes
                .Where(s => s.IdBook == book.IdBook && s.NameScheme == "Главная схема")
                .FirstOrDefault();


            // Если главная схема не найдена, вернуть ошибку
            if (mainScheme == null)
            {
                return NotFound();
            }

            // Получение всех связей, принадлежащих главной схеме
            var connections = _context.Connections.Where(c => c.IdSchemes.Contains(mainScheme));

            // Удаление всех связей, принадлежащих главной схеме
            _context.Connections.RemoveRange(connections);

            // Удаление всех схем
            var schemes = _context.Schemes.Where(s => s.IdBook == book.IdBook);
            _context.Schemes.RemoveRange(schemes);

            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();



            //Удаление всех таймлайнов
            // Получение таймлайна с указанным названием
            var timeline = _context.Timelines
                .Where(t => t.IdBook == book.IdBook && t.NameTimeline == "Главный таймлайн")
                .FirstOrDefault();

            // Если таймлайн не найден, вернуть ошибку
            if (timeline == null)
            {
                return NotFound();
            }

            // Получение всех событий, принадлежащих указанному таймлайну
            var events = _context.Events.Where(e => e.IdTimelines.Contains(timeline));

            // Удаление всех событий, принадлежащих указанному таймлайну
            _context.Events.RemoveRange(events);

            // Получение всех таймлайнов и удаление их
            var timelines = _context.Timelines.Where(t => t.IdBook == book.IdBook);
            _context.Timelines.RemoveRange(timelines);

            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();



            //Удаление всех персонажей, блоков и добавленных атрибутов
            // Создание экземпляра CharacterController
            var characterController = new CharacterController(_context);

            // Получение всех персонажей, связанных с книгой
            var characters = _context.Characters.Where(c => c.IdBook == id);

            // Удаление всех персонажей
            foreach (var character in characters)
            {
                // Вызов метода DeleteCharacter для каждого персонажа
                await characterController.DeleteCharacter(character.IdCharacter);
            }


            //Удаление обложки книги
            var pictureObloshka = _context.Pictures.Find(book.IdPicture);
            if (pictureObloshka != null)
            {
                _context.Pictures.Remove(pictureObloshka);
            }

            // Сохранить изменения в базе данных
            await _context.SaveChangesAsync();


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

        //Получить книгу
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBook([FromBody] string token)
        {
            var userId = _tokenValidator.ValidateToken(token);

            if (userId > 0)
            {
                var user = await _context.Books.FindAsync(userId);

                var userBooks = await _context.BelongToBooks
                    .Where(b => b.IdUser == userId)
                    .Select(b => b.IdBookNavigation)
                    .ToListAsync();

                var bookDtos = userBooks.Select(b => new
                {
                    IdBook = b.IdBook,
                    NameBook = b.NameBook,
                    IdPicture = b.IdPicture
                }).ToList();

                return Ok(bookDtos);
            }
            else
            {
                return Unauthorized("Invalid token");
            }
        }
    }
}
