using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные таймлайна.
    /// </summary>
    public class TimelineData : IMapWith<Timeline>
    {
        /// <summary>
        /// Идентификатор книги, к которой относится таймлайн.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Название таймлайна.
        /// </summary>
        public string NameTimeline { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="TimelineData"/> и <see cref="Timeline"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TimelineData, Timeline>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Если Id генерируется в базе данных, то можно игнорировать
        }
    }
}
