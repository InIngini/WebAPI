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
    /// Данные о пользователе.
    /// </summary>
    public class UserTokenData : IMapWith<User>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Токен аутентификации.
        /// </summary>
        public string Token { get; set; } = null!;

        /// <summary>
        /// Конфигурация сопоставления между <see cref="User"/> и <see cref="UserTokenData"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для создания сопоставлений.</param>

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserTokenData>()
             .ForMember(dest => dest.Token, opt => opt.Ignore()); // Игнорируем, если это не нужно в маппинге
        }
    }
}
