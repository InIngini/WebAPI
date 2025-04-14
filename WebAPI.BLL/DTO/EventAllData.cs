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
    /// Данные полного списка событии.
    /// </summary>
    public class EventAllData
    {
        /// <summary>
        /// Идентификатор события.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название события.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Время события.
        /// </summary>
        public string Time { get; set; }

    }
}
