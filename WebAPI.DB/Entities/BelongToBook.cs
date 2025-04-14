using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    /// <summary>
    /// Класс, представляющий связь между пользователем и книгой.
    /// </summary>
    [PrimaryKey(nameof(UserId), nameof(BookId))]
    public class BelongToBook
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Связанный пользователь.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        /// <summary>
        /// Уникальный идентификатор книги.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Связанная книга.
        /// </summary>
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }

        /// <summary>
        /// Тип принадлежности (например, прочитанная, в библиотеке и т.д.).
        /// </summary>
        public int TypeBelong { get; set; }
    }

}
