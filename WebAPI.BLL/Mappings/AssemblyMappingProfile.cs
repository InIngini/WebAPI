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
    /// Профиль сопоставления для AutoMapper, который применяет сопоставления из типов, реализующих интерфейс <see cref="IMapWith{T}"/>.
    /// </summary>
    public class AssemblyMappingProfile : Profile
    {
        /// <summary>
        /// Инициализируется новый экземпляр класса <see cref="AssemblyMappingProfile"/> и применяет маппинги из указанной сборки.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая типы для сопоставления.</param>
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingFromAssembly(assembly);

        /// <summary>
        /// Применяет сопоставления из всех типов в указанной сборке, которые реализуют интерфейс <see cref="IMapWith{T}"/>.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая типы для сопоставления.</param>
        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            Type[] allTypes = assembly.GetTypes();
            var types = assembly.GetExportedTypes()
                .Where(type=>type.GetInterfaces()
                    .Any(i=>i.IsGenericType&&
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
