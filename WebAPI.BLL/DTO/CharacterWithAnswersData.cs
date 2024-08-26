using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Персонаж с ответами на вопросы по его личностным качествам, внешности, темпераменту и истории.
    /// </summary>
    public class CharacterWithAnswers : IMapWith<Character>
    {
        /// <summary>
        /// Идентификатор изображения персонажа (может быть null).
        /// </summary>
        public int? PictureId { get; set; }

        /// <summary>
        /// Имя персонажа.
        /// </summary>
        public string Name { get; set; }

        // Ответы по личности
        /// <summary> Ответ 1 по личным качествам. </summary>
        public string[] Answers { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="Character"/> и <see cref="CharacterWithAnswers"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Character, CharacterWithAnswers>()
                .ForMember(dest => dest.Answers, opt => opt.Ignore()); // Игнорировать, если Id генерируется в базе данных
        }
    }
}
