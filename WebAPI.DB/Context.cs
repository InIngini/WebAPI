using Microsoft.EntityFrameworkCore;
using WebAPI.DB.CommonAppModel;
using WebAPI.DB.Entities;
using WebAPI.DB.Guide;


namespace WebAPI.DB
{
    /// <summary>
    /// Контекст базы данных для управления сущностями и их связями в базе данных.
    /// </summary>
    public class Context : DbContext, IContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Context"/> с заданными параметрами.
        /// </summary>
        /// <param name="options">Параметры для настройки контекста.</param>
        public Context(DbContextOptions<Context> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            //Database.Migrate();
        }
        /// <summary>
        /// Конфигурирует параметры контекста базы данных.
        /// </summary>
        /// <param name="optionsBuilder">Объект, используемый для задания параметров.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(System.Console.WriteLine);
        }

        /// <summary>
        /// Конфигурирует модель базы данных, определяя связи между сущностями.
        /// </summary>
        /// <param name="modelBuilder">Объект, используемый для настройки модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Character2)
                .WithMany()
                .HasForeignKey(c => c.Character2Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<BelongToScheme>()
                .HasOne(c => c.Scheme)
                .WithMany()
                .HasForeignKey(c => c.SchemeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
        
        public DbSet<SwaggerLogin> SwaggerLogins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BelongToBook> BelongToBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AddedAttribute> AddedAttributes { get; set; }
        public DbSet<BelongToEvent> BelongToEvents { get; set; }
        public DbSet<BelongToScheme> BelongToSchemes { get; set; }
        public DbSet<BelongToTimeline> BelongToTimelines { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<BelongToGallery> BelongToGalleries { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
        public DbSet<Timeline> Timelines { get; set; }
        public DbSet<NumberBlock> NumberBlocks { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Sex> Sex { get; set; }
        public DbSet<TypeBelongToBook> TypeBelongToBooks { get; set; }
        public DbSet<TypeConnection> TypeConnections { get; set; }
    }
}
