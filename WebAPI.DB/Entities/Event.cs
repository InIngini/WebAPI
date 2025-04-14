using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    /// <summary>
    /// Класс, представляющий событие.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Уникальный идентификатор события.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название события.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержимое события, описывающее его особенности или детали.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Время события, представленное в виде строки.
        /// </summary>
        public string Time { get; set; }
    }
}
