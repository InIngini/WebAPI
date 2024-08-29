using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;
using WebAPI.DB.Guide;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using Microsoft.Extensions.Options;


namespace WebAPI.DB
{
    /// <summary>
    /// Контекст базы данных для управления сущностями и их связями в базе данных.
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Context"/> без параметров.
        /// </summary>
        //public Context()
        //{
        //    //Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //    //Database.Migrate();
        //}
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Context"/> с заданными параметрами.
        /// </summary>
        /// <param name="options">Параметры для настройки контекста.</param>
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        /// <summary>
        /// Конфигурирует параметры контекста базы данных.
        /// </summary>
        /// <param name="optionsBuilder">Объект, используемый для задания параметров.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DB;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\VOROB\SOURCE\REPOS\WEBAPI\WEBAPI.DB\BIN\DEBUG\NET8.0\DB\DB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.LogTo(System.Console.WriteLine);
        }

        /// <summary>
        /// Конфигурирует модель базы данных, определяя связи между сущностями.
        /// </summary>
        /// <param name="modelBuilder">Объект, используемый для настройки модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Connection>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd(); // Это обязательно

            modelBuilder.Entity<Connection>()
                .Property(c => c.TypeConnection)
                .IsRequired();

            modelBuilder.Entity<Connection>()
                .Property(c => c.Character1Id)
                .IsRequired();

            modelBuilder.Entity<Connection>()
                .Property(c => c.Character2Id)
                .IsRequired();

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Character2)
                .WithMany()
                .HasForeignKey(c => c.Character2Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BelongToScheme>()
                .HasOne(c => c.Scheme)
                .WithMany()
                .HasForeignKey(c => c.SchemeId)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }
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
