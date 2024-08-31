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
using WebAPI.BLL.Additional;


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
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }


            Book book = _mapper.Map<Book>(userbook);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Создание сущности BelongTo
            Creation.CreateBelongToBook(userbook.UserId, book.Id, "Автор", _context);

            // Создание главной схемы 
            Scheme scheme = new Scheme()
            { 
                NameScheme = "Главная схема",
                BookId = book.Id,
            };
            Creation.CreateScheme(scheme, _context);

            // Создание главного таймлайна
            Timeline timeline = new Timeline()
            { 
                NameTimeline = "Главный таймлайн",
                BookId = book.Id 
            };
            Creation.CreateTimeline(timeline, _context);

            return book;
        }

        /// <summary>
        /// Обновляет существующую книгу.
        /// </summary>
        /// <param name="book">Книга для обновления.</param>
        /// <returns>Обновленная книга.</returns>
        public async Task<Book> UpdateBook(int BookId, Book book)
        {
            var existingBook = await _context.Books.FindAsync(BookId);

            if (existingBook == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Книга", 0));
            }

            existingBook.NameBook = book.NameBook;
            existingBook.PictureId = book.PictureId;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();

            return existingBook;
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
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Книга", 0));
            }

            // Удаление связанных записей из таблицы BelongToBook
            Deletion.DeleteBelongToBook(book.Id, _context);

            // Удаление всех схем книги
            var schemes = await _context.Schemes.Where(s => s.BookId == id).ToListAsync();
            foreach (var scheme in schemes)
            {
                Deletion.DeleteScheme(scheme.Id, _context);
            }

            // Удаление всех таймлайнов книги
            var timelines = await _context.Timelines.Where(s => s.BookId == id).ToListAsync();
            foreach (var timeline in timelines)
            {
                Deletion.DeleteTimeline(timeline.Id, _context);
            }

            // Удаление обложки
            if (book.PictureId != null)
            {
                Deletion.DeletePicture((int)book.PictureId, _context);
            }

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
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Книга", 0));
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
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Пользователь", 1));
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
