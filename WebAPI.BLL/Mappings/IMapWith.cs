using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Mappings
{
    /// <summary>
    /// Интерфейс для объектов, которые могут быть сопоставлены с другими типами.
    /// </summary>
    /// <typeparam name="T">Тип, с которым будет происходить сопоставление.</typeparam>
    public interface IMapWith<T>
    {
        /// <summary>
        /// Конфигурирует сопоставление типов с использованием заданного профиля AutoMapper.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper, который используется для создания сопоставлений.</param>
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T),GetType());
    }
}
