using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Mappings
{
    /// <summary>
    /// Профиль сопоставления для AutoMapper, который применяет сопоставления из типов/>.
    /// Этот профиль автоматически загружает все доступные профили из текущей сборки, 
    /// позволяя избежать ручного добавления каждого отдельного профиля.
    /// </summary>
    public class AssemblyMappingProfile : Profile
    {
        /// <summary>
        /// Конструктор, который извлекает все профили из текущей сборки и добавляет их в конфигурацию AutoMapper.
        /// </summary>
        public AssemblyMappingProfile()
        {
            // Получаем все профили из текущей сборки
            //var profiles = Assembly.GetExecutingAssembly().GetExportedTypes()
            //    .Where(type => typeof(Profile).IsAssignableFrom(type) && !type.IsAbstract);

        }
    }
}
