using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;
using WebAPI.DAL.EF;

namespace WebAPI.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<AddedAttribute> AddedAttributes { get; }
        public IRepository<BelongToBook> BelongToBooks { get; }
        public IRepository<Block1> Block1s { get; }
        public IRepository<Block2> Block2s { get; }
        public IRepository<Block3> Block3s { get; }
        public IRepository<Block4> Block4s { get; }
        public IRepository<Book> Books { get; }
        public IRepository<Character> Characters { get; }
        public IRepository<Connection> Connections { get; }
        public IRepository<Event> Events { get; }
        public IRepository<Gallery> Galleries { get; }
        public IRepository<Picture> Pictures { get; }
        public IRepository<Scheme> Schemes { get; }
        public IRepository<Timeline> Timelines { get; }
        public IRepository<User> Users { get; }
        void Save();
    }
}
