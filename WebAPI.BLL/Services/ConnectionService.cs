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
using WebAPI.BLL.Additional;
using WebAPI.BLL.Errors;
using System.Net;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления связями между персонажами.
    /// </summary>
    public class ConnectionService : IConnectionService
    {
        private readonly IContext Context;
        private readonly IMapper Mapper;
        private CreationRepository CreationRepository { get;}

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConnectionService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public ConnectionService(IContext context, IMapper mapper, CreationRepository creationRepository)
        {
            Context = context;
            Mapper = mapper;
            CreationRepository = creationRepository;
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
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }

            var connection = Mapper.Map<Connection>(connectionData);
            var typeConnection = await Context.TypeConnections
                    .FirstOrDefaultAsync(t => t.Name == connectionData.TypeConnection);
            if (typeConnection == null)
            {
                throw new ApiException(TypesOfErrors.SomethingWentWrong("Нет такого типа связи."));
            }

            connection.TypeConnection = typeConnection.Id;

            await CreationRepository.CreateConnection(connection, (int)connectionData.BookId, Context);

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
            var connection = await Context.Connections.FirstOrDefaultAsync(x => x.Id == id);
            if (connection == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Связь", 0));
            }
            
            var belongToSchemes = await Context.BelongToSchemes.Where(b=>b.ConnectionId == id).ToListAsync();
            // Удаление IdConnection удаляемой связи из схем
            foreach (var belongToScheme in belongToSchemes)
            {
                Context.BelongToSchemes.Remove(belongToScheme);
            }
            await Context.SaveChangesAsync();

            // Удаление связи
            Context.Connections.Remove(connection);
            await Context.SaveChangesAsync();

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
            var connection = await Context.Connections.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (connection == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Связь", 0));
            }
            var connectionData = Mapper.Map<ConnectionData>(connection);
            connectionData.Name1 = (await Context.Characters.FirstOrDefaultAsync(x => x.Id == connection.Character1Id))?.Name;
            connectionData.Name2 = (await Context.Characters.FirstOrDefaultAsync(x => x.Id == connection.Character2Id))?.Name;
            connectionData.TypeConnection = (await Context.TypeConnections.FirstOrDefaultAsync(x => x.Id == connection.TypeConnection))?.Name;

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
            var belongToSchemes = await Context.BelongToSchemes.Where(b=>b.SchemeId == idScheme).ToListAsync(cancellationToken);

            var connections = new List<Connection>();
            foreach (var belongToScheme in belongToSchemes)
            {
                var connection = await Context.Connections.FirstOrDefaultAsync(x => x.Id == belongToScheme.ConnectionId, cancellationToken);
                if (connection==null)
                {
                    throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Связь", 0));
                }
                connections.Add(connection);
            }

            var connectionsData = new List<ConnectionAllData>();

            foreach (var connection in connections)
            {
                var connectionData = Mapper.Map<ConnectionAllData>(connection);
                connectionData.TypeConnection = (await Context.TypeConnections.FirstOrDefaultAsync(x => x.Id == connection.TypeConnection))?.Name;
                connectionsData.Add(connectionData);
            }
            return connectionsData;
        }
    }

}
