﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    /// <summary>
    /// Класс, представляющий схему.
    /// </summary>
    public class Scheme
    {
        /// <summary>
        /// Уникальный идентификатор схемы.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название схемы.
        /// </summary>
        public string NameScheme { get; set; }

        /// <summary>
        /// Идентификатор книги, к которой относится схема.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Связанная книга.
        /// </summary>
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
    }
}
