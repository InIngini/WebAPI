using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DB;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления схемами.
    /// </summary>
    public class SchemeService : ISchemeService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SchemeService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public SchemeService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новую схему.
        /// </summary>
        /// <param name="schemedata">Данные для создания схемы.</param>
        /// <returns>Созданная схема.</returns>
        /// <exception cref="ArgumentException">Если модели не валидны.</exception>
        public async Task<Scheme> CreateScheme(SchemeData schemedata)
        {
            var scheme = _mapper.Map<Scheme>(schemedata);
            var validationContext = new ValidationContext(scheme);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(scheme, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _context.Schemes.Add(scheme);
            _context.SaveChanges();

            return scheme;
        }

        /// <summary>
        /// Обновляет схему и добавляет связь.
        /// </summary>
        /// <param name="scheme">Схема, которую необходимо обновить.</param>
        /// <param name="idConnection">Идентификатор связи.</param>
        /// <returns>Обновленная схема.</returns>
        /// <exception cref="KeyNotFoundException">Если связь не найдена.</exception>
        public async Task<Scheme> UpdateScheme(Scheme scheme, int idConnection)
        {
            var connection = _context.Connections.Find(idConnection);
            if (connection == null)
            {
                throw new KeyNotFoundException();
            }

            // Добавление соединения в схему
            var belongToScheme = new BelongToScheme()
            {
                IdConnection = idConnection,
                IdScheme = scheme.IdScheme
            };
            _context.BelongToSchemes.Add(belongToScheme);
            _context.SaveChanges();

            return scheme;
        }

        /// <summary>
        /// Удаляет схему по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <returns>Удаленная схема.</returns>
        /// <exception cref="KeyNotFoundException">Если схема не найдена.</exception>
        public async Task<Scheme> DeleteScheme(int id)
        {
            var scheme = _context.Schemes.Find(id);

            if (scheme == null)
            {
                throw new KeyNotFoundException();
            }
            var belongToSchemes = _context.BelongToSchemes.Where(b=>b.IdScheme==id).ToList();
            foreach(var belongToScheme in belongToSchemes)
            {
                _context.BelongToSchemes.Remove(belongToScheme);
            }
            _context.SaveChanges();

            _context.Schemes.Remove(scheme);
            _context.SaveChanges();

            return scheme;
        }

        /// <summary>
        /// Получает схему по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор схемы.</param>
        /// <returns>Запрашиваемая схема.</returns>
        /// <exception cref="KeyNotFoundException">Если схема не найдена.</exception>
        public async Task<Scheme> GetScheme(int id)
        {
            var scheme = _context.Schemes.Find(id);

            if (scheme == null)
            {
                throw new KeyNotFoundException();
            }

            return scheme;
        }

        /// <summary>
        /// Получает все схемы для указанной книги.
        /// </summary>
        /// <param name="idBook">Идентификатор книги.</param>
        /// <returns>Список всех схем книги.</returns>
        public async Task<IEnumerable<Scheme>> GetAllSchemes(int idBook)
        {
            var schemes = _context.Schemes.Where(s => s.IdBook == idBook).ToList();

            return schemes;
        }
    }

}
