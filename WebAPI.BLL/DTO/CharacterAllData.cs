using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные полного списка персонажей книги.
    /// </summary>
    public class CharacterAllData : IMapWith<Character>
    {
        /// <summary>
        /// Идентификатор персонажа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя персонажа.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Изображение персонажа в виде массива байтов (может быть null).
        /// </summary>
        public byte[]? PictureContent { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="Character"/> и <see cref="CharacterAllData"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Character, CharacterAllData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.Ignore()) // Игнорирует имя
                .ForMember(dest => dest.PictureContent, opt => opt.Ignore()); // Игнорирует изображение
        }
    }
}
