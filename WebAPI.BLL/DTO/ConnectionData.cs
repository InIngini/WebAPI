﻿using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    /// <summary>
    /// Данные о связи между персонажами и, возможно, их отношение к книге.
    /// </summary>
    public class ConnectionData : IMapWith<Connection>
    {
        /// <summary>
        /// Идентификатор связи (может быть null).
        /// </summary>
        public int? IdConnection { get; set; }

        /// <summary>
        /// Идентификатор книги (может быть null).
        /// </summary>
        public int? IdBook { get; set; }

        /// <summary>
        /// Идентификатор первого персонажа.
        /// </summary>
        public int IdCharacter1 { get; set; }

        /// <summary>
        /// Имя первого персонажа (может быть null).
        /// </summary>
        public string? Name1 { get; set; }

        /// <summary>
        /// Идентификатор второго персонажа.
        /// </summary>
        public int IdCharacter2 { get; set; }

        /// <summary>
        /// Имя второго персонажа (может быть null).
        /// </summary>
        public string? Name2 { get; set; }

        /// <summary>
        /// Тип связи (например: партнер, ребенок-родитель, сиблинг).
        /// </summary>
        public string TypeConnection { get; set; }

        /// <summary>
        /// Конфигурация сопоставления между <see cref="Connection"/> и <see cref="ConnectionData"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Connection, ConnectionData>()
                .ForMember(dest => dest.IdBook, opt => opt.Ignore()) // Игнорируем IdBook
                .ForMember(dest => dest.Name1, opt => opt.Ignore())  // Игнорируем имя первого персонажа
                .ForMember(dest => dest.Name2, opt => opt.Ignore())  // Игнорируем имя второго персонажа
                .ForMember(dest => dest.TypeConnection, opt => opt.Ignore()); // Игнорируем TypeConnection

            profile.CreateMap<ConnectionData, Connection>()
                .ForMember(dest => dest.TypeConnection, opt => opt.Ignore()); // Игнорируем TypeConnection при обратном сопоставлении
        }
    }
}
