using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Entities;
using WebAPI;

namespace WebAPI.DAL.EF;

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

    public virtual DbSet<Answer> Answers { get; set; }

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
        => optionsBuilder.UseSqlServer(ConnectionString.connectionString);


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
            entity.Property(e => e.NumberAnswer).HasColumnName("numberAnswer");

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

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.IdCharacter);

            entity.Property(e => e.IdCharacter)
                .ValueGeneratedNever()
                .HasColumnName("idCharacter");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Answer1Personality).HasColumnName("answer1Personality");
            entity.Property(e => e.Answer2Personality).HasColumnName("answer2Personality");
            entity.Property(e => e.Answer3Personality).HasColumnName("answer3Personality");
            entity.Property(e => e.Answer4Personality).HasColumnName("answer4Personality");
            entity.Property(e => e.Answer5Personality).HasColumnName("answer5Personality");
            entity.Property(e => e.Answer6Personality).HasColumnName("answer6Personality");
            entity.Property(e => e.Answer1Appearance).HasColumnName("answer1Appearance");
            entity.Property(e => e.Answer2Appearance).HasColumnName("answer2Appearance");
            entity.Property(e => e.Answer3Appearance).HasColumnName("answer3Appearance");
            entity.Property(e => e.Answer4Appearance).HasColumnName("answer4Appearance");
            entity.Property(e => e.Answer5Appearance).HasColumnName("answer5Appearance");
            entity.Property(e => e.Answer6Appearance).HasColumnName("answer6Appearance");
            entity.Property(e => e.Answer7Appearance).HasColumnName("answer7Appearance");
            entity.Property(e => e.Answer8Appearance).HasColumnName("answer8Appearance");
            entity.Property(e => e.Answer9Appearance).HasColumnName("answer9Appearance");
            entity.Property(e => e.Answer1Temperament).HasColumnName("answer1Temperament");
            entity.Property(e => e.Answer2Temperament).HasColumnName("answer2Temperament");
            entity.Property(e => e.Answer3Temperament).HasColumnName("answer3Temperament");
            entity.Property(e => e.Answer4Temperament).HasColumnName("answer4Temperament");
            entity.Property(e => e.Answer5Temperament).HasColumnName("answer5Temperament");
            entity.Property(e => e.Answer6Temperament).HasColumnName("answer6Temperament");
            entity.Property(e => e.Answer7Temperament).HasColumnName("answer7Temperament");
            entity.Property(e => e.Answer8Temperament).HasColumnName("answer8Temperament");
            entity.Property(e => e.Answer9Temperament).HasColumnName("answer9Temperament");
            entity.Property(e => e.Answer10Temperament).HasColumnName("answer10Temperament");
            entity.Property(e => e.Answer1ByHistory).HasColumnName("answer1ByHistory");
            entity.Property(e => e.Answer2ByHistory).HasColumnName("answer2ByHistory");
            entity.Property(e => e.Answer3ByHistory).HasColumnName("answer3ByHistory");
            entity.Property(e => e.Answer4ByHistory).HasColumnName("answer4ByHistory");
            entity.Property(e => e.Answer5ByHistory).HasColumnName("answer5ByHistory");

            entity.HasOne(d => d.IdCharacterNavigation).WithOne(p => p.Answer).HasForeignKey<Answer>(d => d.IdCharacter);
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
