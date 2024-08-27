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
using WebAPI.DB.Guide;
using WebAPI.Errors;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления связями между персонажами.
    /// </summary>
    public class ConnectionService : IConnectionService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConnectionService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public ConnectionService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает новую связь между персонажами.
        /// </summary>
        /// <param name="connectionData">Данные для создания связи.</param>
        /// <returns>Созданная связь.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Connection> CreateConnection(ConnectionData connectionData)
        {
            var validationContext = new ValidationContext(connectionData);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(connectionData, validationContext, validationResults, true))
            {
                throw new ArgumentException(TypesOfErrors.NoValidModel());
            }

            var connection = _mapper.Map<Connection>(connectionData);
            connection.TypeConnection = _context.TypeConnections.Where(t => t.Name == connectionData.TypeConnection).FirstOrDefaultAsync().Id;

            var scheme = await _context.Schemes
                            .Where(s => s.NameScheme == "Главная схема" && s.BookId == connectionData.IdBook)
                            .FirstOrDefaultAsync();

            _context.Connections.Add(connection);
            await _context.SaveChangesAsync();

            // Добавление связи в главную схему
            var belongToScheme = new BelongToScheme()
            {
                SchemeId = scheme.Id,
                ConnectionId = connection.Id
            };
            _context.BelongToSchemes.Add(belongToScheme);
            await _context.SaveChangesAsync();

            return connection;
        }

        /// <summary>
        /// Удаляет связь по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор связи.</param>
        /// <returns>Удаленная связь.</returns>
        /// <exception cref="KeyNotFoundException">Если связь не найдена.</exception>
        public async Task<Connection> DeleteConnection(int id)
        {
            var connection = await _context.Connections.FindAsync(id);
            if (connection == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Связь", 0));
            }
            
            var belongToSchemes = await _context.BelongToSchemes.Where(b=>b.ConnectionId == id).ToListAsync();
            // Удаление IdConnection удаляемой связи из схем
            foreach (var belongToScheme in belongToSchemes)
            {
                _context.BelongToSchemes.Remove(belongToScheme);
            }
            await _context.SaveChangesAsync();

            // Удаление связи
            _context.Connections.Remove(connection);
            await _context.SaveChangesAsync();

            return connection;
        }

        /// <summary>
        /// Получает связь по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор связи.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Данные связи.</returns>
        /// <exception cref="KeyNotFoundException">Если связь не найдена.</exception>
        public async Task<ConnectionData> GetConnection(int id, CancellationToken cancellationToken)
        {
            var connection = await _context.Connections.FindAsync(id, cancellationToken);

            if (connection == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Связь", 0));
            }
            var connectionData = _mapper.Map<ConnectionData>(connection);
            connectionData.Name1 = _context.Characters.Find(connection.Character1Id).Name;
            connectionData.Name2 = _context.Characters.Find(connection.Character2Id).Name;
            connectionData.TypeConnection = _context.TypeConnections.Find(connection.TypeConnection).Name;

            return connectionData;
        }

        /// <summary>
        /// Получает все связи для указанной схемы.
        /// </summary>
        /// <param name="idScheme">Идентификатор схемы.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список всех связей.</returns>
        public async Task<IEnumerable<ConnectionAllData>> GetAllConnections(int idScheme, CancellationToken cancellationToken)
        {
            var belongToSchemes = await _context.BelongToSchemes.Where(b=>b.SchemeId == idScheme).ToListAsync(cancellationToken);

            var connections = new List<Connection>();
            foreach (var belongToScheme in belongToSchemes)
            {
                var connection = await _context.Connections.FindAsync(belongToScheme.ConnectionId, cancellationToken);
                if (connection==null)
                {
                    throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Связь", 0));
                }
                connections.Add(connection);
            }

            var connectionsData = new List<ConnectionAllData>();

            foreach (var connection in connections)
            {
                var connectionData = _mapper.Map<ConnectionAllData>(connection);
                connectionData.TypeConnection = _context.TypeConnections.Find(connection.TypeConnection).Name;
                connectionsData.Add(connectionData);
            }
            return connectionsData;
        }
    }

}
