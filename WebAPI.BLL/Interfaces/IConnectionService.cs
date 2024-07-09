using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface IConnectionService
    {
        Task<Connection> CreateConnection(Connection connection);
        Task<Connection> DeleteConnection(int id);
        Task<Connection> GetConnection(int id);
        Task<IEnumerable<Connection>> GetAllConnections(int idScheme);
    }

}
