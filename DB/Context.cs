using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;

namespace DB
{
    public class Context : DbContext
    {
        public Context() 
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DB;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=|DataDirectory|\DB\DB.mdf;Integrated Security=True;");
            optionsBuilder.LogTo(System.Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Character2)
                .WithMany()
                .HasForeignKey(c => c.idCharacter2)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<BelongToScheme>()
                .HasOne(c => c.Scheme)
                .WithMany()
                .HasForeignKey(c => c.idScheme)
                .OnDelete(DeleteBehavior.ClientSetNull);


        }
        public DbSet<User> Users { get; set; }
        public DbSet<BelongToBook> BelongToBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AddedAttribute> AddedAttributes { get; set; }
        public DbSet<BelongToEvent> BelongToEvents { get; set; }
        public DbSet<BelongToScheme> BelongToSchemes { get; set; }
        public DbSet<BelongToTimeline> BelongToTimelines { get; set; }
        public DbSet<Block1> Block1s { get; set; }
        public DbSet<Block2> Block2s { get; set; }
        public DbSet<Block3> Block3s { get; set; }
        public DbSet<Block4> Block4s { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
        public DbSet<Timeline> Timelines { get; set; }
    }
}
