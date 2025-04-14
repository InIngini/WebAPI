using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные о книге пользователя.
    /// </summary>
    public class UserBookData
    {
        /// <summary>
        /// Идентификатор пользователя, которому принадлежит книга.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Название книги.
        /// </summary>
        public string NameBook { get; set; }

        /// <summary>
        /// Идентификатор изображения книги (может быть null).
        /// </summary>
        public int? PictureId { get; set; }

    }
}
