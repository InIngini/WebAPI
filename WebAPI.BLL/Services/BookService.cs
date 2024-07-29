using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using AutoMapper;


namespace WebAPI.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> CreateBook(UserBookData userbook)
        {
            var validationContext = new ValidationContext(userbook);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(userbook, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }


            Book book = _mapper.Map<Book>(userbook);

            _unitOfWork.Books.Create(book);
            _unitOfWork.Save();

            // Создание сущности BelongTo
            BelongToBook belongToBook = new BelongToBook
            {
                IdUser = userbook.idUser,
                IdBook = book.IdBook,
                TypeBelong = 1 // автор
            };

            // Сохранение BelongTo в базе данных
            _unitOfWork.BelongToBooks.Create(belongToBook);
            _unitOfWork.Save();

            // Создание главной схемы 
            Scheme scheme = new Scheme()
            {
                NameScheme = "Главная схема",
                IdBook = book.IdBook,
            };
            _unitOfWork.Schemes.Create(scheme);
            _unitOfWork.Save();

            // Создание главного таймлайна
            Timeline timeline = new Timeline()
            {
                NameTimeline = "Главный таймлайн",
                IdBook = book.IdBook,
            };
            _unitOfWork.Timelines.Create(timeline);
            _unitOfWork.Save();

            return book;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            _unitOfWork.Books.Update(book);
            _unitOfWork.Save();

            return book;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var book = _unitOfWork.Books.Get(id);

            if (book == null)
            {
                throw new KeyNotFoundException();
            }

            // Удаление связанных записей из таблицы BelongToBook
            _unitOfWork.BelongToBooks.Delete(id);
            _unitOfWork.Save();

            // Удаление всех схем книги
            var schemes = _unitOfWork.Schemes.Find(s => s.IdBook == id);
            foreach (var scheme in schemes)
            {
                if (scheme.NameScheme == "Главная схема")
                {
                    // Удаление всех связей схемы
                    var belongToScheme = _unitOfWork.BelongToSchemes.Find(b=> b.IdScheme==scheme.IdScheme);
                    foreach (var connection in belongToScheme)
                    {
                        _unitOfWork.Connections.Delete(connection.IdConnection);
                        _unitOfWork.BelongToSchemes.Delete(connection.IdConnection);
                    }
                }
                // Удаление схемы
                _unitOfWork.Schemes.Delete(scheme.IdScheme);
            }
            _unitOfWork.Save();

            // Удаление всех таймлайнов книги
            var timelines = _unitOfWork.Timelines.Find(s => s.IdBook == id);
            foreach (var timeline in timelines)
            {
                if (timeline.NameTimeline == "Главый таймлайн")
                {
                    // Удаление всех связей схемы
                    var belongToTimeline = _unitOfWork.BelongToTimelines.Find(b => b.IdTimeline == timeline.IdTimeline);
                    foreach (var @event in belongToTimeline)
                    {
                        _unitOfWork.Events.Delete(@event.IdEvent);
                        _unitOfWork.BelongToTimelines.Delete(@event.IdEvent);
                    }
                }
                // Удаление схемы
                _unitOfWork.Timelines.Delete(timeline.IdTimeline);
            }
            _unitOfWork.Save();

            // Удаление обложки
            if (book.IdPicture != null)
            {
                int idP = (int)book.IdPicture;
                var image = _unitOfWork.Pictures.Get(idP);
                _unitOfWork.Pictures.Delete(image.IdPicture);
            }
            _unitOfWork.Save();

            // Удаление всех персонажей книги
            var characters = _unitOfWork.Characters.Find(c => c.IdBook == id);
            CharacterService characterService = new CharacterService(_unitOfWork,_mapper);
            foreach (var character in characters)
            {
                characterService.DeleteCharacter(character.IdCharacter);
            }

            // Удаление книги
            _unitOfWork.Books.Delete(id);
            _unitOfWork.Save();

            return book;
        }

        public async Task<Book> GetBook(int id)
        {
            var book = _unitOfWork.Books.Get(id);

            if (book == null)
            {
                throw new KeyNotFoundException();
            }

            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooksForUser(int userId)
        {
            var user = _unitOfWork.Users.Get(userId);
            if (user == null)
            {
                throw new ArgumentException("User with the specified ID does not exist.");
            }
            //var books = _unitOfWork.Books.Find(b => b.BelongToBooks.Any(u => u.IdUser == userId)).ToList();
            var books = new List<Book>();
            var belongToBooks = _unitOfWork.BelongToBooks.Find(b=>b.IdUser == userId);
            foreach (var belongToBook in belongToBooks)
            {
                var book = _unitOfWork.Books.Get(belongToBook.IdBook);
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
