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
    public class UserTokenData
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
    }
}
