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
                throw new ArgumentException("Модель не валидна");
            }


            Book book = _mapper.Map<Book>(userbook);
            _context.Books.Add(book);
            _context.SaveChanges();

            // Создание сущности BelongTo
            BelongToBook belongToBook = new BelongToBook
            {
                IdUser = userbook.IdUser,
                IdBook = book.IdBook,
                TypeBelong = 1 // автор
            };
            _context.BelongToBooks.Add(belongToBook);
            _context.SaveChanges();

            // Создание главной схемы 
            Scheme scheme = new Scheme()
            {
                NameScheme = "Главная схема",
                IdBook = book.IdBook,
            };
            _context.Schemes.Add(scheme);
            _context.SaveChanges();

            // Создание главного таймлайна
            Timeline timeline = new Timeline()
            {
                NameTimeline = "Главный таймлайн",
                IdBook = book.IdBook,
            };
            _context.Timelines.Add(timeline);
            _context.SaveChanges();

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
            _context.SaveChanges();

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
            var book = _context.Books.Find(id);
            if (book == null)
            {
                throw new KeyNotFoundException();
            }

            // Удаление связанных записей из таблицы BelongToBook
            var belongToBook = _context.BelongToBooks.Where(b=>b.IdBook== book.IdBook).FirstOrDefault();
            if (belongToBook == null)
            {
                throw new KeyNotFoundException();
            }
            _context.BelongToBooks.Remove(belongToBook);
            _context.SaveChanges();

            // Удаление всех схем книги
            var schemes = _context.Schemes.Where(s => s.IdBook == id).ToList();
            foreach (var scheme in schemes)
            {
                if (scheme.NameScheme == "Главная схема")
                {
                    // Удаление всех связей схемы
                    var belongToSchemes = _context.BelongToSchemes.Where(b => b.IdScheme == scheme.IdScheme).ToList();
                    foreach (var belongToScheme in belongToSchemes)
                    {
                        _context.BelongToSchemes.Remove(belongToScheme);

                        var connection = _context.Connections.Find(belongToScheme.IdConnection);
                        if (connection == null)
                        {
                            throw new KeyNotFoundException();
                        }
                        _context.Connections.Remove(connection);
                        
                    }
                }
                // Удаление схемы
                _context.Schemes.Remove(scheme);
            }
            _context.SaveChanges();

            // Удаление всех таймлайнов книги
            var timelines = _context.Timelines.Where(s => s.IdBook == id).ToList();
            foreach (var timeline in timelines)
            {
                if (timeline.NameTimeline == "Главый таймлайн")
                {
                    // Удаление всех связей схемы
                    var belongToTimelines = _context.BelongToTimelines.Where(b => b.IdTimeline == timeline.IdTimeline).ToList();
                    foreach (var belongToTimeline in belongToTimelines)
                    {
                        _context.BelongToTimelines.Remove(belongToTimeline);
                        var @event = _context.Events.Find(belongToTimeline.IdEvent);
                        if (@event == null)
                        {
                            throw new KeyNotFoundException();
                        }
                        _context.Events.Remove(@event);
                    }
                }
                // Удаление схемы
                _context.Timelines.Remove(timeline);
            }
            _context.SaveChanges();

            // Удаление обложки
            if (book.IdPicture != null)
            {
                int idP = (int)book.IdPicture;
                var image = _context.Pictures.Find(idP);
                _context.Pictures.Remove(image);
            }
            _context.SaveChanges();

            // Удаление всех персонажей книги
            var characters = _context.Characters.Where(c => c.IdBook == id).ToList();
            CharacterService characterService = new CharacterService(_context,_mapper);
            foreach (var character in characters)
            {
                await characterService.DeleteCharacter(character.IdCharacter);
            }

            // Удаление книги
            _context.Books.Remove(book);
            _context.SaveChanges();

            return book;
        }

        /// <summary>
        /// Получает книгу по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Книга с указанным идентификатором.</returns>
        /// <exception cref="KeyNotFoundException">Если книга не найдена.</exception>
        public async Task<Book> GetBook(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                throw new KeyNotFoundException();
            }

            return book;
        }

        /// <summary>
        /// Получает все книги, связанные с указанным пользователем.
        /// </summary>
        /// <param name="idUser">Идентификатор пользователя.</param>
        /// <returns>Список книг, принадлежащих пользователю.</returns>
        /// <exception cref="ArgumentException">Если пользователь с указанным идентификатором не существует.</exception>
        public async Task<IEnumerable<Book>> GetAllBooksForUser(int idUser)
        {
            var user = _context.Users.Find(idUser);
            if (user == null)
            {
                throw new ArgumentException("User with the specified ID does not exist.");
            }
            //var books = _context.Books.Where(b => b.BelongToBooks.Any(u => u.IdUser == userId)).ToList();
            var books = new List<Book>();
            var belongToBooks = _context.BelongToBooks.Where(b=>b.IdUser == idUser).ToList();
            foreach (var belongToBook in belongToBooks)
            {
                var book = _context.Books.Find(belongToBook.IdBook);
                if (book != null)
                {
                    var bookDtos = new Book
                    {
                        IdBook = book.IdBook,
                        NameBook = book.NameBook,
                        IdPicture = book.IdPicture
                    };
                    books.Add(bookDtos);
                }
            }

            return books;
        }
    }

}
