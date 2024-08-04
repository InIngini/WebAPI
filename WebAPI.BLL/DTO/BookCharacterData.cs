using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные для создания персонажа книги.
    /// </summary>
    public class BookCharacterData : IMapWith<Character>
    {
        /// <summary>
        /// Идентификатор книги.
        /// </summary>
        public int IdBook { get; set; }

        /// <summary>
        /// Идентификатор изображения персонажа (может быть null).
        /// </summary>
        public int? IdPicture { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="BookCharacterData"/> и <see cref="Character/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookCharacterData, Character>()
                .ForMember(dest => dest.IdBook, opt => opt.MapFrom(src => src.IdBook))
                .ForMember(dest => dest.IdPicture, opt => opt.MapFrom(src => src.IdPicture))
                .ForMember(dest => dest.IdCharacter, opt => opt.Ignore()); // Игнорировать, если Id генерируется в базе данных
        }
    }
}
