﻿// <auto-generated />
using System;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DB.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DB.Entities.AddedAttribute", b =>
                {
                    b.Property<int>("idAttribute")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idAttribute"));

                    b.Property<string>("contentAttribute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idCharacter")
                        .HasColumnType("int");

                    b.Property<string>("nameAttribute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numberBlock")
                        .HasColumnType("int");

                    b.HasKey("idAttribute");

                    b.HasIndex("idCharacter");

                    b.ToTable("AddedAttributes");
                });

            modelBuilder.Entity("DB.Entities.BelongToBook", b =>
                {
                    b.Property<int>("idUser")
                        .HasColumnType("int");

                    b.Property<int>("idBook")
                        .HasColumnType("int");

                    b.Property<int>("typeBelong")
                        .HasColumnType("int");

                    b.HasKey("idUser", "idBook");

                    b.HasIndex("idBook");

                    b.ToTable("BelongToBooks");
                });

            modelBuilder.Entity("DB.Entities.BelongToEvent", b =>
                {
                    b.Property<int>("idCharacter")
                        .HasColumnType("int");

                    b.Property<int>("idEvent")
                        .HasColumnType("int");

                    b.HasKey("idCharacter", "idEvent");

                    b.HasIndex("idEvent");

                    b.ToTable("BelongToEvents");
                });

            modelBuilder.Entity("DB.Entities.BelongToScheme", b =>
                {
                    b.Property<int>("idScheme")
                        .HasColumnType("int");

                    b.Property<int>("idConnection")
                        .HasColumnType("int");

                    b.HasKey("idScheme", "idConnection");

                    b.HasIndex("idConnection");

                    b.ToTable("BelongToSchemes");
                });

            modelBuilder.Entity("DB.Entities.BelongToTimeline", b =>
                {
                    b.Property<int>("idTimeline")
                        .HasColumnType("int");

                    b.Property<int>("idEvent")
                        .HasColumnType("int");

                    b.HasKey("idTimeline", "idEvent");

                    b.HasIndex("idEvent");

                    b.ToTable("BelongToTimelines");
                });

            modelBuilder.Entity("DB.Entities.Block1", b =>
                {
                    b.Property<int>("idCharacter")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCharacter");

                    b.ToTable("Block1s");
                });

            modelBuilder.Entity("DB.Entities.Block2", b =>
                {
                    b.Property<int>("idCharacter")
                        .HasColumnType("int");

                    b.Property<string>("question1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question8")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question9")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCharacter");

                    b.ToTable("Block2s");
                });

            modelBuilder.Entity("DB.Entities.Block3", b =>
                {
                    b.Property<int>("idCharacter")
                        .HasColumnType("int");

                    b.Property<string>("question1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question10")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question8")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question9")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCharacter");

                    b.ToTable("Block3s");
                });

            modelBuilder.Entity("DB.Entities.Block4", b =>
                {
                    b.Property<int>("idCharacter")
                        .HasColumnType("int");

                    b.Property<string>("question1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCharacter");

                    b.ToTable("Block4s");
                });

            modelBuilder.Entity("DB.Entities.Book", b =>
                {
                    b.Property<int>("idBook")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idBook"));

                    b.Property<int?>("idPicture")
                        .HasColumnType("int");

                    b.Property<string>("nameBook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idBook");

                    b.HasIndex("idPicture");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("DB.Entities.Character", b =>
                {
                    b.Property<int>("idCharacter")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCharacter"));

                    b.Property<int>("idBook")
                        .HasColumnType("int");

                    b.Property<int?>("idPicture")
                        .HasColumnType("int");

                    b.HasKey("idCharacter");

                    b.HasIndex("idBook");

                    b.HasIndex("idPicture");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("DB.Entities.Connection", b =>
                {
                    b.Property<int>("idConnection")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idConnection"));

                    b.Property<int>("idCharacter1")
                        .HasColumnType("int");

                    b.Property<int>("idCharacter2")
                        .HasColumnType("int");

                    b.Property<int>("typeConnection")
                        .HasColumnType("int");

                    b.HasKey("idConnection");

                    b.HasIndex("idCharacter1");

                    b.HasIndex("idCharacter2");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("DB.Entities.Event", b =>
                {
                    b.Property<int>("idEvent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEvent"));

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idEvent");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DB.Entities.Gallery", b =>
                {
                    b.Property<int?>("idPicture")
                        .HasColumnType("int");

                    b.Property<int>("idCharacter")
                        .HasColumnType("int");

                    b.HasKey("idPicture");

                    b.HasIndex("idCharacter");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("DB.Entities.Picture", b =>
                {
                    b.Property<int>("idPicture")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPicture"));

                    b.Property<byte[]>("picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("idPicture");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("DB.Entities.Scheme", b =>
                {
                    b.Property<int>("idScheme")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idScheme"));

                    b.Property<int>("idBook")
                        .HasColumnType("int");

                    b.Property<string>("nameScheme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idScheme");

                    b.HasIndex("idBook");

                    b.ToTable("Schemes");
                });

            modelBuilder.Entity("DB.Entities.Timeline", b =>
                {
                    b.Property<int>("idTimeline")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idTimeline"));

                    b.Property<int>("idBook")
                        .HasColumnType("int");

                    b.Property<string>("nameTimeline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idTimeline");

                    b.HasIndex("idBook");

                    b.ToTable("Timelines");
                });

            modelBuilder.Entity("DB.Entities.User", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUser"));

                    b.Property<string>("login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DB.Entities.AddedAttribute", b =>
                {
                    b.HasOne("DB.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("idCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DB.Entities.BelongToBook", b =>
                {
                    b.HasOne("DB.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("idBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DB.Entities.BelongToEvent", b =>
                {
                    b.HasOne("DB.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("idCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("idEvent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("DB.Entities.BelongToScheme", b =>
                {
                    b.HasOne("DB.Entities.Connection", "Connection")
                        .WithMany()
                        .HasForeignKey("idConnection")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Entities.Scheme", "Scheme")
                        .WithMany()
                        .HasForeignKey("idScheme")
                        .IsRequired();

                    b.Navigation("Connection");

                    b.Navigation("Scheme");
                });

            modelBuilder.Entity("DB.Entities.BelongToTimeline", b =>
                {
                    b.HasOne("DB.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("idEvent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Entities.Timeline", "Timeline")
                        .WithMany()
                        .HasForeignKey("idTimeline")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Timeline");
                });

            modelBuilder.Entity("DB.Entities.Block1", b =>
                {
                    b.HasOne("DB.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("idCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DB.Entities.Block2", b =>
                {
                    b.HasOne("DB.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("idCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DB.Entities.Block3", b =>
                {
                    b.HasOne("DB.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("idCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DB.Entities.Block4", b =>
                {
                    b.HasOne("DB.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("idCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DB.Entities.Book", b =>
                {
                    b.HasOne("DB.Entities.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("idPicture");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("DB.Entities.Character", b =>
                {
                    b.HasOne("DB.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("idBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Entities.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("idPicture");

                    b.Navigation("Book");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("DB.Entities.Connection", b =>
                {
                    b.HasOne("DB.Entities.Character", "Character1")
                        .WithMany()
                        .HasForeignKey("idCharacter1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Entities.Character", "Character2")
                        .WithMany()
                        .HasForeignKey("idCharacter2")
                        .IsRequired();

                    b.Navigation("Character1");

                    b.Navigation("Character2");
                });

            modelBuilder.Entity("DB.Entities.Gallery", b =>
                {
                    b.HasOne("DB.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("idCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Entities.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("idPicture")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("DB.Entities.Scheme", b =>
                {
                    b.HasOne("DB.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("idBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("DB.Entities.Timeline", b =>
                {
                    b.HasOne("DB.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("idBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });
#pragma warning restore 612, 618
        }
    }
}
