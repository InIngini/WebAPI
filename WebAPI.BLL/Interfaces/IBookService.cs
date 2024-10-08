﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface IBookService
    {
        Task<Book> CreateBook(UserBookData userbook);
        Task<Book> UpdateBook(int id,Book book);
        Task<Book> DeleteBook(int id);
        Task<Book> GetBook(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetAllBooksForUser(int userId, CancellationToken cancellationToken);
    }

}
