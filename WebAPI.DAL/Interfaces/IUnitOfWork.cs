using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;
using WebAPI.DB;
using WebAPI.DB.Guide;

namespace WebAPI.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<AddedAttribute> AddedAttributes { get; }
        public IRepository<BelongToBook> BelongToBooks { get; }
        public IRepository<BelongToEvent> BelongToEvents { get; }
        public IRepository<BelongToScheme> BelongToSchemes { get; }
        public IRepository<BelongToTimeline> BelongToTimelines { get; }
        public IRepository<Answer> Answers { get; }
        public IRepository<Book> Books { get; }
        public IRepository<Character> Characters { get; }
        public IRepository<Connection> Connections { get; }
        public IRepository<Event> Events { get; }
        public IRepository<Gallery> Galleries { get; }
        public IRepository<Picture> Pictures { get; }
        public IRepository<Scheme> Schemes { get; }
        public IRepository<Timeline> Timelines { get; }
        public IRepository<User> Users { get; }
        public IRepository<NumberBlock> NumberBlocks { get; }
        public IRepository<Question> Questions { get; }
        public IRepository<Sex> Sex { get; }
        public IRepository<TypeBelongToBook> TypeBelongToBooks { get; }
        public IRepository<TypeConnection> TypeConnections { get; }
        void Save();
    }
}
