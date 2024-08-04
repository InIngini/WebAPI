using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Персонаж с ответами на вопросы по его личностным качествам, внешности, темпераменту и истории.
    /// </summary>
    public class CharacterWithAnswers : IMapWith<Answer>
    {
        /// <summary>
        /// Идентификатор изображения персонажа (может быть null).
        /// </summary>
        public int? IdPicture { get; set; }

        /// <summary>
        /// Имя персонажа.
        /// </summary>
        public string Name { get; set; }

        // Ответы по личности
        /// <summary> Ответ 1 по личным качествам. </summary>
        public string Answer1Personality { get; set; }
        /// <summary> Ответ 2 по личным качествам. </summary>
        public string Answer2Personality { get; set; }
        /// <summary> Ответ 3 по личным качествам. </summary>
        public string Answer3Personality { get; set; }
        /// <summary> Ответ 4 по личным качествам. </summary>
        public string Answer4Personality { get; set; }
        /// <summary> Ответ 5 по личным качествам. </summary>
        public string Answer5Personality { get; set; }
        /// <summary> Ответ 6 по личным качествам. </summary>
        public string Answer6Personality { get; set; }

        // Ответы по внешности
        /// <summary> Ответ 1 по внешности. </summary>
        public string Answer1Appearance { get; set; }
        /// <summary> Ответ 2 по внешности. </summary>
        public string Answer2Appearance { get; set; }
        /// <summary> Ответ 3 по внешности. </summary>
        public string Answer3Appearance { get; set; }
        /// <summary> Ответ 4 по внешности. </summary>
        public string Answer4Appearance { get; set; }
        /// <summary> Ответ 5 по внешности. </summary>
        public string Answer5Appearance { get; set; }
        /// <summary> Ответ 6 по внешности. </summary>
        public string Answer6Appearance { get; set; }
        /// <summary> Ответ 7 по внешности. </summary>
        public string Answer7Appearance { get; set; }
        /// <summary> Ответ 8 по внешности. </summary>
        public string Answer8Appearance { get; set; }
        /// <summary> Ответ 9 по внешности. </summary>
        public string Answer9Appearance { get; set; }

        // Ответы по темпераменту
        /// <summary> Ответ 1 по темпераменту. </summary>
        public string Answer1Temperament { get; set; }
        /// <summary> Ответ 2 по темпераменту. </summary>
        public string Answer2Temperament { get; set; }
        /// <summary> Ответ 3 по темпераменту. </summary>
        public string Answer3Temperament { get; set; }
        /// <summary> Ответ 4 по темпераменту. </summary>
        public string Answer4Temperament { get; set; }
        /// <summary> Ответ 5 по темпераменту. </summary>
        public string Answer5Temperament { get; set; }
        /// <summary> Ответ 6 по темпераменту. </summary>
        public string Answer6Temperament { get; set; }
        /// <summary> Ответ 7 по темпераменту. </summary>
        public string Answer7Temperament { get; set; }
        /// <summary> Ответ 8 по темпераменту. </summary>
        public string Answer8Temperament { get; set; }
        /// <summary> Ответ 9 по темпераменту. </summary>
        public string Answer9Temperament { get; set; }
        /// <summary> Ответ 10 по темпераменту. </summary>
        public string Answer10Temperament { get; set; }

        // Ответы по истории
        /// <summary> Ответ 1 по истории персонажа. </summary>
        public string Answer1ByHistory { get; set; }
        /// <summary> Ответ 2 по истории персонажа. </summary>
        public string Answer2ByHistory { get; set; }
        /// <summary> Ответ 3 по истории персонажа. </summary>
        public string Answer3ByHistory { get; set; }
        /// <summary> Ответ 4 по истории персонажа. </summary>
        public string Answer4ByHistory { get; set; }
        /// <summary> Ответ 5 по истории персонажа. </summary>
        public string Answer5ByHistory { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="Answer"/> и <see cref="CharacterWithAnswers"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Answer, CharacterWithAnswers>(); // Игнорировать, если Id генерируется в базе данных
        }
    }
}
