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


namespace WebAPI.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> CreateBook(Book book)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Books.Create(book);
            _unitOfWork.Save();

            // Создание сущности BelongTo
            BelongToBook belongToBook = new BelongToBook
            {
                IdUser = bookdata.idUser,
                IdBook = book.IdBook,
                TypeBelong = 0 // Установить значение по умолчанию для статуса
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
            var books = await _unitOfWork.Books.Find(b => b.BelongToBooks.Any(u => u.IdUser == userId)).ToListAsync();

            return books;
        }
    }

}
