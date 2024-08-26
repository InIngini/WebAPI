using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные для добавленного атрибута.
    /// </summary>
    public class AAData : IMapWith<AddedAttribute>
    {
        /// <summary>
        /// Номер ответа атрибута.
        /// </summary>
        public int NumberAnswer { get; set; }

        /// <summary>
        /// Имя атрибута.
        /// </summary>
        public string NameAttribute { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="AAData"/> и <see cref="AddedAttribute"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AAData, AddedAttribute>()
                .ForMember(dest => dest.ContentAttribute, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CharacterId, opt => opt.Ignore()); // Игнорировать, если Id генерируется в базе данных
        }
    }
}
