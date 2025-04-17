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
using System.Net;


namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления книгами.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IContext Context;
        private readonly IMapper Mapper;
        private DeletionRepository DeletionRepository;
        private CreationRepository CreationRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public BookService(IContext context, IMapper mapper, DeletionRepository deletionRepository, CreationRepository creationRepository)
        {
            Context = context;
            Mapper = mapper;
            DeletionRepository = deletionRepository;
            CreationRepository = creationRepository;
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

            Book book = Mapper.Map<Book>(userbook);
            Context.Books.Add(book);
            await Context.SaveChangesAsync();

            // Создание сущности BelongTo
            await CreationRepository.CreateBelongToBook(userbook.UserId, book.Id, "Автор", Context);

            // Создание главной схемы 
            Scheme scheme = new Scheme()
            {
                NameScheme = "Главная схема",
                BookId = book.Id,
            };
            await CreationRepository.CreateScheme(scheme, Context);

            // Создание главного таймлайна
            Timeline timeline = new Timeline()
            {
                NameTimeline = "Главный таймлайн",
                BookId = book.Id
            };
            await CreationRepository.CreateTimeline(timeline, Context);

            return book;
        }

        /// <summary>
        /// Обновляет существующую книгу.
        /// </summary>
        /// <param name="book">Книга для обновления.</param>
        /// <returns>Обновленная книга.</returns>
        public async Task<Book> UpdateBook(int userId, int BookId, Book book)
        {
            var existingBook = await Context.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            if (existingBook == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Книга", 0));
            }

            var belongToBook = await Context.BelongToBooks.FirstOrDefaultAsync(x => x.BookId == BookId && x.UserId == userId);
            if (belongToBook == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotYour("Книга", 0));
            }

            existingBook.NameBook = book.NameBook;
            existingBook.PictureId = book.PictureId;

            Context.Books.Update(existingBook);
            await Context.SaveChangesAsync();

            return existingBook;
        }

        /// <summary>
        /// Удаляет книгу по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Удаленная книга.</returns>
        /// <exception cref="KeyNotFoundException">Если книга не найдена.</exception>
        public async Task<Book> DeleteBook(int userId, int id)
        {
            var book = await Context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Книга", 0));
            }

            var belongToBook = await Context.BelongToBooks.FirstOrDefaultAsync(x => x.BookId == id && x.UserId == userId);
            if (belongToBook == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotYour("Книга", 0));
            }

            // Удаление связанных записей из таблицы BelongToBook
            await DeletionRepository.DeleteBelongToBook(book.Id, Context);

            // Удаление всех схем книги
            var schemes = await Context.Schemes.Where(s => s.BookId == id).ToListAsync();
            foreach (var scheme in schemes)
            {
                await DeletionRepository.DeleteScheme(scheme.Id, Context);
            }

            // Удаление всех таймлайнов книги
            var timelines = await Context.Timelines.Where(s => s.BookId == id).ToListAsync();
            foreach (var timeline in timelines)
            {
                await DeletionRepository.DeleteTimeline(timeline.Id, Context);
            }

            // Удаление обложки
            if (book.PictureId != null)
            {
                await DeletionRepository.DeletePicture((int)book.PictureId, Context);
            }

            // Удаление всех персонажей книги
            var characters = await Context.Characters.Where(c => c.BookId == id).ToListAsync();
            CharacterService characterService = new CharacterService(Context, Mapper, DeletionRepository, CreationRepository);
            foreach (var character in characters)
            {
                await characterService.DeleteCharacter(character.Id);
            }

            // Удаление книги
            Context.Books.Remove(book);
            await Context.SaveChangesAsync();

            return book;
        }

        /// <summary>
        /// Получает книгу по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Книга с указанным идентификатором.</returns>
        /// <exception cref="KeyNotFoundException">Если книга не найдена.</exception>
        public async Task<Book> GetBook(int userId, int id, CancellationToken cancellationToken)
        {
            var belongToBook = await Context.BelongToBooks.FirstOrDefaultAsync(x => x.BookId == id && x.UserId == userId);
            if (belongToBook == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotYour("Книга", 0));
            }

            var book = await Context.Books.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

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
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == idUser);
            if (user == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Пользователь", 1));
            }
            //var books = await Context.Books.Where(b => b.BelongToBooks.Any(u => u.IdUser == userId)).ToListAsync();
            var books = new List<Book>();
            var belongToBooks = await Context.BelongToBooks.Where(b => b.UserId == idUser).ToListAsync(cancellationToken);
            foreach (var belongToBook in belongToBooks)
            {
                var book = await Context.Books.FirstOrDefaultAsync(x => x.Id == belongToBook.BookId);
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
