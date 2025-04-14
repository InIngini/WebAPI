using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    /// <summary>
    /// Класс, представляющий пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Логин пользователя для входа.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя для входа.
        /// </summary>
        public string Password { get; set; }
    }
}
