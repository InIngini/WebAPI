using System;
using WebAPI.DAL.EF;
using WebAPI.DAL.Interfaces;
using WebAPI.DAL.Entities;
using WebAPI.DAL.Guide;

namespace WebAPI.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private Context db;
        private AddedAttributeRepository addedAttributeRepository;
        private BelongToBookRepository belongToBookRepository;
        private AnswerRepository answerRepository;
        private BookRepository bookRepository;
        private CharacterRepository characterRepository;
        private ConnectionRepository connectionRepository;
        private EventRepository eventRepository;
        private GalleryRepository galleryRepository;
        private PictureRepository pictureRepository;
        private SchemeRepository schemeRepository;
        private TimelineRepository timelineRepository;
        private UserRepository userRepository;
        private NumberBlockRepository numberBlockRepository;
        private QuestionRepository questionRepository;
        private SexRepository sexRepository;
        private TypeBelongToBookRepository typeBelongToBookRepository;
        private TypeConnectionRepository typeConnectionRepository;

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

        public IRepository<Answer> Answers
        {
            get
            {
                if (answerRepository == null)
                    answerRepository = new AnswerRepository(db);
                return answerRepository;
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

        public IRepository<NumberBlock> NumberBlocks
        {
            get
            {
                if (numberBlockRepository == null)
                    numberBlockRepository = new NumberBlockRepository(db);
                return numberBlockRepository;
            }
        }

        public IRepository<Question> Questions
        {
            get
            {
                if (questionRepository == null)
                    questionRepository = new QuestionRepository(db);
                return questionRepository;
            }
        }

        public IRepository<Sex> Sex
        {
            get
            {
                if (sexRepository == null)
                    sexRepository = new SexRepository(db);
                return sexRepository;
            }
        }

        public IRepository<TypeBelongToBook> TypeBelongToBooks
        {
            get
            {
                if (typeBelongToBookRepository == null)
                    typeBelongToBookRepository = new TypeBelongToBookRepository(db);
                return typeBelongToBookRepository;
            }
        }

        public IRepository<TypeConnection> TypeConnections
        {
            get
            {
                if (typeConnectionRepository == null)
                    typeConnectionRepository = new TypeConnectionRepository(db);
                return typeConnectionRepository;
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
