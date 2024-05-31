using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI;

public partial class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AddedAttribute> AddedAttributes { get; set; }

    public virtual DbSet<BelongToBook> BelongToBooks { get; set; }

    public virtual DbSet<Block1> Block1s { get; set; }

    public virtual DbSet<Block2> Block2s { get; set; }

    public virtual DbSet<Block3> Block3s { get; set; }

    public virtual DbSet<Block4> Block4s { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Gallery> Galleries { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Scheme> Schemes { get; set; }

    public virtual DbSet<Timeline> Timelines { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Program.connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddedAttribute>(entity =>
        {
            entity.HasKey(e => e.IdAttribute);

            entity.HasIndex(e => e.IdCharacter, "IX_AddedAttributes_idCharacter");

            entity.Property(e => e.IdAttribute).HasColumnName("idAttribute");
            entity.Property(e => e.ContentAttribute).HasColumnName("contentAttribute");
            entity.Property(e => e.IdCharacter).HasColumnName("idCharacter");
            entity.Property(e => e.NameAttribute).HasColumnName("nameAttribute");
            entity.Property(e => e.NumberBlock).HasColumnName("numberBlock");

            entity.HasOne(d => d.IdCharacterNavigation).WithMany(p => p.AddedAttributes).HasForeignKey(d => d.IdCharacter);
        });

        modelBuilder.Entity<BelongToBook>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdBook });

            entity.HasIndex(e => e.IdBook, "IX_BelongToBooks_idBook");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.IdBook).HasColumnName("idBook");
            entity.Property(e => e.TypeBelong).HasColumnName("typeBelong");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.BelongToBooks).HasForeignKey(d => d.IdBook);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.BelongToBooks).HasForeignKey(d => d.IdUser);
        });

        modelBuilder.Entity<Block1>(entity =>
        {
            entity.HasKey(e => e.IdCharacter);

            entity.Property(e => e.IdCharacter)
                .ValueGeneratedNever()
                .HasColumnName("idCharacter");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Question1).HasColumnName("question1");
            entity.Property(e => e.Question2).HasColumnName("question2");
            entity.Property(e => e.Question3).HasColumnName("question3");
            entity.Property(e => e.Question4).HasColumnName("question4");
            entity.Property(e => e.Question5).HasColumnName("question5");
            entity.Property(e => e.Question6).HasColumnName("question6");

            entity.HasOne(d => d.IdCharacterNavigation).WithOne(p => p.Block1).HasForeignKey<Block1>(d => d.IdCharacter);
        });

        modelBuilder.Entity<Block2>(entity =>
        {
            entity.HasKey(e => e.IdCharacter);

            entity.Property(e => e.IdCharacter)
                .ValueGeneratedNever()
                .HasColumnName("idCharacter");
            entity.Property(e => e.Question1).HasColumnName("question1");
            entity.Property(e => e.Question2).HasColumnName("question2");
            entity.Property(e => e.Question3).HasColumnName("question3");
            entity.Property(e => e.Question4).HasColumnName("question4");
            entity.Property(e => e.Question5).HasColumnName("question5");
            entity.Property(e => e.Question6).HasColumnName("question6");
            entity.Property(e => e.Question7).HasColumnName("question7");
            entity.Property(e => e.Question8).HasColumnName("question8");
            entity.Property(e => e.Question9).HasColumnName("question9");

            entity.HasOne(d => d.IdCharacterNavigation).WithOne(p => p.Block2).HasForeignKey<Block2>(d => d.IdCharacter);
        });

        modelBuilder.Entity<Block3>(entity =>
        {
            entity.HasKey(e => e.IdCharacter);

            entity.Property(e => e.IdCharacter)
                .ValueGeneratedNever()
                .HasColumnName("idCharacter");
            entity.Property(e => e.Question1).HasColumnName("question1");
            entity.Property(e => e.Question10).HasColumnName("question10");
            entity.Property(e => e.Question2).HasColumnName("question2");
            entity.Property(e => e.Question3).HasColumnName("question3");
            entity.Property(e => e.Question4).HasColumnName("question4");
            entity.Property(e => e.Question5).HasColumnName("question5");
            entity.Property(e => e.Question6).HasColumnName("question6");
            entity.Property(e => e.Question7).HasColumnName("question7");
            entity.Property(e => e.Question8).HasColumnName("question8");
            entity.Property(e => e.Question9).HasColumnName("question9");

            entity.HasOne(d => d.IdCharacterNavigation).WithOne(p => p.Block3).HasForeignKey<Block3>(d => d.IdCharacter);
        });

        modelBuilder.Entity<Block4>(entity =>
        {
            entity.HasKey(e => e.IdCharacter);

            entity.Property(e => e.IdCharacter)
                .ValueGeneratedNever()
                .HasColumnName("idCharacter");
            entity.Property(e => e.Question1).HasColumnName("question1");
            entity.Property(e => e.Question2).HasColumnName("question2");
            entity.Property(e => e.Question3).HasColumnName("question3");
            entity.Property(e => e.Question4).HasColumnName("question4");
            entity.Property(e => e.Question5).HasColumnName("question5");

            entity.HasOne(d => d.IdCharacterNavigation).WithOne(p => p.Block4).HasForeignKey<Block4>(d => d.IdCharacter);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook);

            entity.HasIndex(e => e.IdPicture, "IX_Books_idPicture");

            entity.Property(e => e.IdBook).HasColumnName("idBook");
            entity.Property(e => e.IdPicture).HasColumnName("idPicture");
            entity.Property(e => e.NameBook).HasColumnName("nameBook");

            entity.HasOne(d => d.IdPictureNavigation).WithMany(p => p.Books).HasForeignKey(d => d.IdPicture);
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.IdCharacter);

            entity.HasIndex(e => e.IdBook, "IX_Characters_idBook");

            entity.HasIndex(e => e.IdPicture, "IX_Characters_idPicture");

            entity.Property(e => e.IdCharacter).HasColumnName("idCharacter");
            entity.Property(e => e.IdBook).HasColumnName("idBook");
            entity.Property(e => e.IdPicture).HasColumnName("idPicture");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Characters).HasForeignKey(d => d.IdBook);

            entity.HasOne(d => d.IdPictureNavigation).WithMany(p => p.Characters).HasForeignKey(d => d.IdPicture);

            entity.HasMany(d => d.IdEvents).WithMany(p => p.IdCharacters)
                .UsingEntity<Dictionary<string, object>>(
                    "BelongToEvent",
                    r => r.HasOne<Event>().WithMany().HasForeignKey("IdEvent"),
                    l => l.HasOne<Character>().WithMany().HasForeignKey("IdCharacter"),
                    j =>
                    {
                        j.HasKey("IdCharacter", "IdEvent");
                        j.ToTable("BelongToEvents");
                        j.HasIndex(new[] { "IdEvent" }, "IX_BelongToEvents_idEvent");
                        j.IndexerProperty<int>("IdCharacter").HasColumnName("idCharacter");
                        j.IndexerProperty<int>("IdEvent").HasColumnName("idEvent");
                    });
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.IdConnection);

            entity.HasIndex(e => e.IdCharacter1, "IX_Connections_idCharacter1");

            entity.HasIndex(e => e.IdCharacter2, "IX_Connections_idCharacter2");

            entity.Property(e => e.IdConnection).HasColumnName("idConnection");
            entity.Property(e => e.IdCharacter1).HasColumnName("idCharacter1");
            entity.Property(e => e.IdCharacter2).HasColumnName("idCharacter2");
            entity.Property(e => e.TypeConnection).HasColumnName("typeConnection");

            entity.HasOne(d => d.IdCharacter1Navigation).WithMany(p => p.ConnectionIdCharacter1Navigations).HasForeignKey(d => d.IdCharacter1);

            entity.HasOne(d => d.IdCharacter2Navigation).WithMany(p => p.ConnectionIdCharacter2Navigations)
                .HasForeignKey(d => d.IdCharacter2)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.IdEvent);

            entity.Property(e => e.IdEvent).HasColumnName("idEvent");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<Gallery>(entity =>
        {
            entity.HasKey(e => e.IdPicture);

            entity.HasIndex(e => e.IdCharacter, "IX_Galleries_idCharacter");

            entity.Property(e => e.IdPicture)
                .ValueGeneratedNever()
                .HasColumnName("idPicture");
            entity.Property(e => e.IdCharacter).HasColumnName("idCharacter");

            entity.HasOne(d => d.IdCharacterNavigation).WithMany(p => p.Galleries).HasForeignKey(d => d.IdCharacter);

            entity.HasOne(d => d.IdPictureNavigation).WithOne(p => p.Gallery).HasForeignKey<Gallery>(d => d.IdPicture);
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.IdPicture);

            entity.Property(e => e.IdPicture).HasColumnName("idPicture");
            entity.Property(e => e.Picture1).HasColumnName("picture");
        });

        modelBuilder.Entity<Scheme>(entity =>
        {
            entity.HasKey(e => e.IdScheme);

            entity.HasIndex(e => e.IdBook, "IX_Schemes_idBook");

            entity.Property(e => e.IdScheme).HasColumnName("idScheme");
            entity.Property(e => e.IdBook).HasColumnName("idBook");
            entity.Property(e => e.NameScheme).HasColumnName("nameScheme");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Schemes).HasForeignKey(d => d.IdBook);

            entity.HasMany(d => d.IdConnections).WithMany(p => p.IdSchemes)
                .UsingEntity<Dictionary<string, object>>(
                    "BelongToScheme",
                    r => r.HasOne<Connection>().WithMany().HasForeignKey("IdConnection"),
                    l => l.HasOne<Scheme>().WithMany()
                        .HasForeignKey("IdScheme")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("IdScheme", "IdConnection");
                        j.ToTable("BelongToSchemes");
                        j.HasIndex(new[] { "IdConnection" }, "IX_BelongToSchemes_idConnection");
                        j.IndexerProperty<int>("IdScheme").HasColumnName("idScheme");
                        j.IndexerProperty<int>("IdConnection").HasColumnName("idConnection");
                    });
        });

        modelBuilder.Entity<Timeline>(entity =>
        {
            entity.HasKey(e => e.IdTimeline);

            entity.HasIndex(e => e.IdBook, "IX_Timelines_idBook");

            entity.Property(e => e.IdTimeline).HasColumnName("idTimeline");
            entity.Property(e => e.IdBook).HasColumnName("idBook");
            entity.Property(e => e.NameTimeline).HasColumnName("nameTimeline");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Timelines).HasForeignKey(d => d.IdBook);

            entity.HasMany(d => d.IdEvents).WithMany(p => p.IdTimelines)
                .UsingEntity<Dictionary<string, object>>(
                    "BelongToTimeline",
                    r => r.HasOne<Event>().WithMany().HasForeignKey("IdEvent"),
                    l => l.HasOne<Timeline>().WithMany().HasForeignKey("IdTimeline"),
                    j =>
                    {
                        j.HasKey("IdTimeline", "IdEvent");
                        j.ToTable("BelongToTimelines");
                        j.HasIndex(new[] { "IdEvent" }, "IX_BelongToTimelines_idEvent");
                        j.IndexerProperty<int>("IdTimeline").HasColumnName("idTimeline");
                        j.IndexerProperty<int>("IdEvent").HasColumnName("idEvent");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
