using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;
using WebAPI.BLL.DTO;

namespace WebAPI.BLL.Interfaces
{
    public interface IConnectionService
    {
        Task<Connection> CreateConnection(ConnectionData connectionData);
        Task<Connection> DeleteConnection(int id);
        Task<ConnectionData> GetConnection(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ConnectionAllData>> GetAllConnections(int idScheme, CancellationToken cancellationToken);
    }

}
