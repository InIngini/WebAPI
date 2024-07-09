using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;

namespace WebAPI.BLL.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConnectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Connection> CreateConnection(Connection connection)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Connections.Create(connection);
            _unitOfWork.Save();

            return connection;
        }

        public async Task<Connection> DeleteConnection(int id)
        {
            // Получение связи из базы данных
            var connection = _unitOfWork.Connections.Get(id);

            // Если связь не найдена, вернуть ошибку
            if (connection == null)
            {
                throw new KeyNotFoundException();
            }

            // Получение схем, связанных со связью, и удаление их оттуда
            var schemes = await _unitOfWork.Schemes.GetAll().ToListAsync();

            // Удаление IdConnection удаляемой связи из схем
            foreach (var scheme in schemes)
            {
                var connection1 = scheme.IdConnections.FirstOrDefault(c => c.IdConnection == id);
                if (connection1 != null)
                {
                    scheme.IdConnections.Remove(connection1);
                }
            }

            // Сохранение изменений в базе данных
            _unitOfWork.Save();

            // Удаление связи
            _unitOfWork.Connections.Delete(id);
            _unitOfWork.Save();

            return connection;
        }

        public async Task<Connection> GetConnection(int id)
        {
            var connection = _unitOfWork.Connections.Get(id);

            if (connection == null)
            {
                throw new KeyNotFoundException();
            }

            return connection;
        }

        public async Task<IEnumerable<Connection>> GetAllConnections(int idScheme)
        {
            var connections = await _unitOfWork.Connections.Find(c => c.IdSchemes.Any(s => s.IdScheme == idScheme)).ToListAsync();

            return connections;
        }
    }

}
