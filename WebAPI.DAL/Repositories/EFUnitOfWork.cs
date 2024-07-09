using System;
using WebAPI.DAL.EF;
using WebAPI.DAL.Interfaces;
using WebAPI.DAL.Entities;

namespace WebAPI.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private Context db;
        private AddedAttributeRepository addedAttributeRepository;
        private BelongToBookRepository belongToBookRepository;
        private Block1Repository block1Repository;
        private Block2Repository block2Repository;
        private Block3Repository block3Repository;
        private Block4Repository block4Repository;
        private BookRepository bookRepository;
        private CharacterRepository characterRepository;
        private ConnectionRepository connectionRepository;
        private EventRepository eventRepository;
        private GalleryRepository galleryRepository;
        private PictureRepository pictureRepository;
        private SchemeRepository schemeRepository;
        private TimelineRepository timelineRepository;
        private UserRepository userRepository;

        public EFUnitOfWork()
        {
            db = new Context();
        }

        public IRepository<AddedAttribute> AddedAttributes
        {
            get
            {
                if (addedAttributeRepository == null)
                    addedAttributeRepository = new AddedAttributeRepository(db);
                return addedAttributeRepository;
            }
        }

        public IRepository<BelongToBook> BelongToBooks
        {
            get
            {
                if (belongToBookRepository == null)
                    belongToBookRepository = new BelongToBookRepository(db);
                return belongToBookRepository;
            }
        }

        public IRepository<Block1> Block1s
        {
            get
            {
                if (block1Repository == null)
                    block1Repository = new Block1Repository(db);
                return block1Repository;
            }
        }

        public IRepository<Block2> Block2s
        {
            get
            {
                if (block2Repository == null)
                    block2Repository = new Block2Repository(db);
                return block2Repository;
            }
        }

        public IRepository<Block3> Block3s
        {
            get
            {
                if (block3Repository == null)
                    block3Repository = new Block3Repository(db);
                return block3Repository;
            }
        }

        public IRepository<Block4> Block4s
        {
            get
            {
                if (block4Repository == null)
                    block4Repository = new Block4Repository(db);
                return block4Repository;
            }
        }

        public IRepository<Book> Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public IRepository<Character> Characters
        {
            get
            {
                if (characterRepository == null)
                    characterRepository = new CharacterRepository(db);
                return characterRepository;
            }
        }

        public IRepository<Connection> Connections
        {
            get
            {
                if (connectionRepository == null)
                    connectionRepository = new ConnectionRepository(db);
                return connectionRepository;
            }
        }

        public IRepository<Event> Events
        {
            get
            {
                if (eventRepository == null)
                    eventRepository = new EventRepository(db);
                return eventRepository;
            }
        }

        public IRepository<Gallery> Galleries
        {
            get
            {
                if (galleryRepository == null)
                    galleryRepository = new GalleryRepository(db);
                return galleryRepository;
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                if (pictureRepository == null)
                    pictureRepository = new PictureRepository(db);
                return pictureRepository;
            }
        }

        public IRepository<Scheme> Schemes
        {
            get
            {
                if (schemeRepository == null)
                    schemeRepository = new SchemeRepository(db);
                return schemeRepository;
            }
        }

        public IRepository<Timeline> Timelines
        {
            get
            {
                if (timelineRepository == null)
                    timelineRepository = new TimelineRepository(db);
                return timelineRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
