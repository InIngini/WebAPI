using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные схемы.
    /// </summary>
    public class SchemeData : IMapWith<Scheme>
    {
        /// <summary>
        /// Идентификатор книги, к которой относится схема.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Название схемы.
        /// </summary>
        public string NameScheme { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="SchemeData"/> и <see cref="Scheme"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SchemeData, Scheme>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
