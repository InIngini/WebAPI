﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    /// <summary>
    /// Класс, представляющий изображение.
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// Уникальный идентификатор изображения.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Двоичные данные изображения.
        /// </summary>
        public byte[] PictureContent { get; set; }
    }
}
