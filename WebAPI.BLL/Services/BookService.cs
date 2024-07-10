using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace WebAPI.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> CreateBook(UserBookData userbook)
        {
            var validationContext = new ValidationContext(userbook);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(userbook, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            Book book = new Book()
            { 
                NameBook = userbook.NameBook,
                IdPicture = userbook.IdPicture
            };

            _unitOfWork.Books.Create(book);
            _unitOfWork.Save();

            // Создание сущности BelongTo
            BelongToBook belongToBook = new BelongToBook
            {
                IdUser = userbook.idUser,
                IdBook = book.IdBook,
                TypeBelong = 0 // автор
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
                    var connections = _unitOfWork.Connections.Find(c => c.IdSchemes.Any(s => s.IdScheme == scheme.IdScheme));
                    foreach (var connection in connections)
                    {
                        _unitOfWork.Connections.Delete(connection.IdConnection);
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
                    var events = _unitOfWork.Events.Find(c => c.IdTimelines.Any(s => s.IdTimeline == timeline.IdTimeline));
                    foreach (var @event in events)
                    {
                        _unitOfWork.Connections.Delete(@event.IdEvent);
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
            CharacterService characterService = new CharacterService(_unitOfWork);
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
            var books = _unitOfWork.Books.Find(b => b.BelongToBooks.Any(u => u.IdUser == userId)).ToList();

            return books;
        }
    }

}
