using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные о книге пользователя.
    /// </summary>
    public class UserBookData : IMapWith<Book>
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

        /// <summary>
        /// Конфигурация сопоставления между <see cref="UserBookData"/> и <see cref="Book"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserBookData, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
