using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DB;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebAPI.Errors;


namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления книгами.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public BookService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новую книгу и связывает её с пользователем.
        /// </summary>
        /// <param name="userbook">Данные книги от пользователя.</param>
        /// <returns>Созданная книга.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Book> CreateBook(UserBookData userbook)
        {
            var validationContext = new ValidationContext(userbook);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(userbook, validationContext, validationResults, true))
            {
                throw new ArgumentException(TypesOfErrors.NoValidModel());
            }


            Book book = _mapper.Map<Book>(userbook);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Создание сущности BelongTo
            BelongToBook belongToBook = new BelongToBook
            {
                UserId = userbook.UserId,
                BookId = book.Id,
                TypeBelong = 1 // автор
            };
            _context.BelongToBooks.Add(belongToBook);
            await _context.SaveChangesAsync();

            // Создание главной схемы 
            Scheme scheme = new Scheme()
            {
                NameScheme = "Главная схема",
                BookId = book.Id,
            };
            _context.Schemes.Add(scheme);
            await _context.SaveChangesAsync();

            // Создание главного таймлайна
            Timeline timeline = new Timeline()
            {
                NameTimeline = "Главный таймлайн",
                BookId = book.Id,
            };
            _context.Timelines.Add(timeline);
            await _context.SaveChangesAsync();

            return book;
        }

        /// <summary>
        /// Обновляет существующую книгу.
        /// </summary>
        /// <param name="book">Книга для обновления.</param>
        /// <returns>Обновленная книга.</returns>
        public async Task<Book> UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return book;
        }

        /// <summary>
        /// Удаляет книгу по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Удаленная книга.</returns>
        /// <exception cref="KeyNotFoundException">Если книга не найдена.</exception>
        public async Task<Book> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Книга", 0));
            }

            // Удаление связанных записей из таблицы BelongToBook
            var belongToBook = await _context.BelongToBooks.Where(b=>b.BookId== book.Id).FirstOrDefaultAsync();
            if (belongToBook == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Книга", 0));
            }
            _context.BelongToBooks.Remove(belongToBook);
            await _context.SaveChangesAsync();

            // Удаление всех схем книги
            var schemes = await _context.Schemes.Where(s => s.BookId == id).ToListAsync();
            foreach (var scheme in schemes)
            {
                if (scheme.NameScheme == "Главная схема")
                {
                    // Удаление всех связей схемы
                    var belongToSchemes = await _context.BelongToSchemes.Where(b => b.SchemeId == scheme.Id).ToListAsync();
                    foreach (var belongToScheme in belongToSchemes)
                    {
                        _context.BelongToSchemes.Remove(belongToScheme);

                        var connection = await _context.Connections.FindAsync(belongToScheme.ConnectionId);
                        if (connection == null)
                        {
                            throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Связи из главной схемы", 3));
                        }
                        _context.Connections.Remove(connection);
                        
                    }
                }
                // Удаление схемы
                _context.Schemes.Remove(scheme);
            }
            await _context.SaveChangesAsync();

            // Удаление всех таймлайнов книги
            var timelines = await _context.Timelines.Where(s => s.BookId == id).ToListAsync();
            foreach (var timeline in timelines)
            {
                if (timeline.NameTimeline == "Главый таймлайн")
                {
                    // Удаление всех связей схемы
                    var belongToTimelines = await _context.BelongToTimelines.Where(b => b.TimelineId == timeline.Id).ToListAsync();
                    foreach (var belongToTimeline in belongToTimelines)
                    {
                        _context.BelongToTimelines.Remove(belongToTimeline);
                        var @event = await _context.Events.FindAsync(belongToTimeline.EventId);
                        if (@event == null)
                        {
                            throw new KeyNotFoundException(TypesOfErrors.NoFoundById("События из главного таймлайна", 3));
                        }
                        _context.Events.Remove(@event);
                    }
                }
                // Удаление схемы
                _context.Timelines.Remove(timeline);
            }
            await _context.SaveChangesAsync();

            // Удаление обложки
            if (book.PictureId != null)
            {
                int idP = (int)book.PictureId;
                var image = await _context.Pictures.FindAsync(idP);
                _context.Pictures.Remove(image);
            }
            await _context.SaveChangesAsync();

            // Удаление всех персонажей книги
            var characters = await _context.Characters.Where(c => c.BookId == id).ToListAsync();
            CharacterService characterService = new CharacterService(_context,_mapper);
            foreach (var character in characters)
            {
                await characterService.DeleteCharacter(character.Id);
            }

            // Удаление книги
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        /// <summary>
        /// Получает книгу по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Книга с указанным идентификатором.</returns>
        /// <exception cref="KeyNotFoundException">Если книга не найдена.</exception>
        public async Task<Book> GetBook(int id, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(id,cancellationToken);

            if (book == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Книга", 0));
            }

            return book;
        }

        /// <summary>
        /// Получает все книги, связанные с указанным пользователем.
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список книг, принадлежащих пользователю.</returns>
        /// <exception cref="ArgumentException">Если пользователь с указанным идентификатором не существует.</exception>
        public async Task<IEnumerable<Book>> GetAllBooksForUser(int idUser, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(idUser);
            if (user == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Пользователь", 1));
            }
            //var books = await _context.Books.Where(b => b.BelongToBooks.Any(u => u.IdUser == userId)).ToListAsync();
            var books = new List<Book>();
            var belongToBooks = await _context.BelongToBooks.Where(b=>b.UserId == idUser).ToListAsync(cancellationToken);
            foreach (var belongToBook in belongToBooks)
            {
                var book = await _context.Books.FindAsync(belongToBook.BookId);
                if (book != null)
                {
                    var bookDtos = new Book
                    {
                        Id = book.Id,
                        NameBook = book.NameBook,
                        PictureId = book.PictureId
                    };
                    books.Add(bookDtos);
                }
            }

            return books;
        }
    }

}
