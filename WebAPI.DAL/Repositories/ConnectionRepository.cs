using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.EF;
using WebAPI.DAL.Entities;
using WebAPI.DAL.Interfaces;

namespace WebAPI.DAL.Repositories
{
    public class ConnectionRepository : IRepository<Connection>
    {
        private Context db;

        public ConnectionRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Connection> GetAll()
        {
            return db.Connections;
        }

        public Connection Get(int id)
        {
            return db.Connections.Find(id);
        }

        public void Create(Connection connection)
        {
            db.Connections.Add(connection);
        }

        public void Update(Connection connection)
        {
            db.Entry(connection).State = EntityState.Modified;
        }

        public IEnumerable<Connection> Find(Func<Connection, Boolean> predicate)
        {
            return db.Connections.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Connection connection = db.Connections.Find(id);
            if (connection != null)
                db.Connections.Remove(connection);
        }
    }
}
