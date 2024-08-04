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
    /// <summary>
    /// Интерфейс для работы с единицей работы (Unit of Work), 
    /// которая управляет несколькими репозиториями и обеспечением транзакционного контекста.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Репозиторий для добавленных атрибутов.
        /// </summary>
        public IRepository<AddedAttribute> AddedAttributes { get; }

        /// <summary>
        /// Репозиторий для принадлежностей книге.
        /// </summary>
        public IRepository<BelongToBook> BelongToBooks { get; }

        /// <summary>
        /// Репозиторий для принадлежностей событий.
        /// </summary>
        public IRepository<BelongToEvent> BelongToEvents { get; }

        /// <summary>
        /// Репозиторий для принадлежностей схеме.
        /// </summary>
        public IRepository<BelongToScheme> BelongToSchemes { get; }

        /// <summary>
        /// Репозиторий для принадлежностей временным линиям.
        /// </summary>
        public IRepository<BelongToTimeline> BelongToTimelines { get; }

        /// <summary>
        /// Репозиторий для ответов.
        /// </summary>
        public IRepository<Answer> Answers { get; }

        /// <summary>
        /// Репозиторий для книг.
        /// </summary>
        public IRepository<Book> Books { get; }

        /// <summary>
        /// Репозиторий для персонажей.
        /// </summary>
        public IRepository<Character> Characters { get; }

        /// <summary>
        /// Репозиторий для связей.
        /// </summary>
        public IRepository<Connection> Connections { get; }

        /// <summary>
        /// Репозиторий для событий.
        /// </summary>
        public IRepository<Event> Events { get; }

        /// <summary>
        /// Репозиторий для галерей.
        /// </summary>
        public IRepository<Gallery> Galleries { get; }

        /// <summary>
        /// Репозиторий для изображений.
        /// </summary>
        public IRepository<Picture> Pictures { get; }

        /// <summary>
        /// Репозиторий для схем.
        /// </summary>
        public IRepository<Scheme> Schemes { get; }

        /// <summary>
        /// Репозиторий для временных линий.
        /// </summary>
        public IRepository<Timeline> Timelines { get; }

        /// <summary>
        /// Репозиторий для пользователей.
        /// </summary>
        public IRepository<User> Users { get; }

        /// <summary>
        /// Репозиторий для блоков.
        /// </summary>
        public IRepository<NumberBlock> NumberBlocks { get; }

        /// <summary>
        /// Репозиторий для вопросов.
        /// </summary>
        public IRepository<Question> Questions { get; }

        /// <summary>
        /// Репозиторий для пола.
        /// </summary>
        public IRepository<Sex> Sex { get; }

        /// <summary>
        /// Репозиторий для типов принадлежностей книг.
        /// </summary>
        public IRepository<TypeBelongToBook> TypeBelongToBooks { get; }

        /// <summary>
        /// Репозиторий для типов связей.
        /// </summary>
        public IRepository<TypeConnection> TypeConnections { get; }

        /// <summary>
        /// Сохранить изменения в контекст базы данных.
        /// </summary>
        void Save();
    }
}
