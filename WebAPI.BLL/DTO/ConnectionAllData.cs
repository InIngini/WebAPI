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
    /// Данные о связи между персонажами.
    /// </summary>
    public class ConnectionAllData : IMapWith<Connection>
    {
        /// <summary>
        /// Идентификатор связи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор первого персонажа в связи.
        /// </summary>
        public int Character1Id { get; set; }

        /// <summary>
        /// Идентификатор второго персонажа в связи.
        /// </summary>
        public int Character2Id { get; set; }

        /// <summary>
        /// Тип связи (например: партнер, ребенок-родитель, сиблинг).
        /// </summary>
        public string TypeConnection { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="Connection"/> и <see cref="ConnectionAllData"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Connection, ConnectionAllData>()
                .ForMember(dest => dest.TypeConnection, opt => opt.Ignore()); // Игнорируем TypeConnection, если значения генерируются
        }
    }
}
