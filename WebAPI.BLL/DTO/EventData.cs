using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные о событии, включая информацию о связанных книгах и персонажах.
    /// </summary>
    public class EventData : IMapWith<Event>
    {
        /// <summary>
        /// Идентификатор книги (может быть null).
        /// </summary>
        public int? BookId { get; set; }

        /// <summary>
        /// Название события.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержимое события.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Время события.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Массив идентификаторов персонажей, участвующих в событии (может быть null).
        /// </summary>
        public int[]? CharactersId { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="Event"/> и <see cref="EventData"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            // Создаём сопоставление между сущностью Event и классом EventData
            profile.CreateMap<Event, EventData>();

            // Создаём обратное сопоставление от EventData к Event
            profile.CreateMap<EventData, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Игнорируем IdEvent, если он генерируется в базе данных
        }
    }
}
