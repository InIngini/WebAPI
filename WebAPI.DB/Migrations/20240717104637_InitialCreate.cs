using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.DB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    IdEvent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.IdEvent);
                });

            migrationBuilder.CreateTable(
                name: "NumberBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    IdPicture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture1 = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.IdPicture);
                });

            migrationBuilder.CreateTable(
                name: "Sex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeBelongToBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeBelongToBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Block = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_NumberBlocks_Block",
                        column: x => x.Block,
                        principalTable: "NumberBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    IdBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameBook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPicture = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.IdBook);
                    table.ForeignKey(
                        name: "FK_Books_Pictures_IdPicture",
                        column: x => x.IdPicture,
                        principalTable: "Pictures",
                        principalColumn: "IdPicture");
                });

            migrationBuilder.CreateTable(
                name: "BelongToBooks",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdBook = table.Column<int>(type: "int", nullable: false),
                    TypeBelong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToBooks", x => new { x.IdUser, x.IdBook });
                    table.ForeignKey(
                        name: "FK_BelongToBooks_Books_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Books",
                        principalColumn: "IdBook",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BelongToBooks_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    IdCharacter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBook = table.Column<int>(type: "int", nullable: false),
                    IdPicture = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.IdCharacter);
                    table.ForeignKey(
                        name: "FK_Characters_Books_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Books",
                        principalColumn: "IdBook",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Pictures_IdPicture",
                        column: x => x.IdPicture,
                        principalTable: "Pictures",
                        principalColumn: "IdPicture");
                });

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    IdScheme = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameScheme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.IdScheme);
                    table.ForeignKey(
                        name: "FK_Schemes_Books_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Books",
                        principalColumn: "IdBook",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timelines",
                columns: table => new
                {
                    IdTimeline = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTimeline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timelines", x => x.IdTimeline);
                    table.ForeignKey(
                        name: "FK_Timelines_Books_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Books",
                        principalColumn: "IdBook",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddedAttributes",
                columns: table => new
                {
                    IdAttribute = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberAnswer = table.Column<int>(type: "int", nullable: false),
                    NnameAttribute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAttribute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCharacter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddedAttributes", x => x.IdAttribute);
                    table.ForeignKey(
                        name: "FK_AddedAttributes_Characters_IdCharacter",
                        column: x => x.IdCharacter,
                        principalTable: "Characters",
                        principalColumn: "IdCharacter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    IdCharacter = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer1Personality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer2Personality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer3Personality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer4Personality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer5Personality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer6Personality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer1Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer2Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer3Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer4Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer5Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer6Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer7Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer8Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer9Appearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer1Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer2Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer3Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer4Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer5Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer6Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer7Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer8Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer9Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer10Temperament = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer1ByHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer2ByHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer3ByHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer4ByHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer5ByHistory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.IdCharacter);
                    table.ForeignKey(
                        name: "FK_Answers_Characters_IdCharacter",
                        column: x => x.IdCharacter,
                        principalTable: "Characters",
                        principalColumn: "IdCharacter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BelongToEvents",
                columns: table => new
                {
                    IdCharacter = table.Column<int>(type: "int", nullable: false),
                    IdEvent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToEvents", x => new { x.IdCharacter, x.IdEvent });
                    table.ForeignKey(
                        name: "FK_BelongToEvents_Characters_IdCharacter",
                        column: x => x.IdCharacter,
                        principalTable: "Characters",
                        principalColumn: "IdCharacter",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BelongToEvents_Events_IdEvent",
                        column: x => x.IdEvent,
                        principalTable: "Events",
                        principalColumn: "IdEvent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    IdConnection = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeConnection = table.Column<int>(type: "int", nullable: false),
                    IdCharacter1 = table.Column<int>(type: "int", nullable: false),
                    IdCharacter2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.IdConnection);
                    table.ForeignKey(
                        name: "FK_Connections_Characters_IdCharacter1",
                        column: x => x.IdCharacter1,
                        principalTable: "Characters",
                        principalColumn: "IdCharacter",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Characters_IdCharacter2",
                        column: x => x.IdCharacter2,
                        principalTable: "Characters",
                        principalColumn: "IdCharacter");
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    IdPicture = table.Column<int>(type: "int", nullable: false),
                    IdCharacter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.IdPicture);
                    table.ForeignKey(
                        name: "FK_Galleries_Characters_IdCharacter",
                        column: x => x.IdCharacter,
                        principalTable: "Characters",
                        principalColumn: "IdCharacter",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Galleries_Pictures_IdPicture",
                        column: x => x.IdPicture,
                        principalTable: "Pictures",
                        principalColumn: "IdPicture",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BelongToTimelines",
                columns: table => new
                {
                    IdTimeline = table.Column<int>(type: "int", nullable: false),
                    IdEvent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToTimelines", x => new { x.IdTimeline, x.IdEvent });
                    table.ForeignKey(
                        name: "FK_BelongToTimelines_Events_IdEvent",
                        column: x => x.IdEvent,
                        principalTable: "Events",
                        principalColumn: "IdEvent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BelongToTimelines_Timelines_IdTimeline",
                        column: x => x.IdTimeline,
                        principalTable: "Timelines",
                        principalColumn: "IdTimeline",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BelongToSchemes",
                columns: table => new
                {
                    IdScheme = table.Column<int>(type: "int", nullable: false),
                    IdConnection = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToSchemes", x => new { x.IdScheme, x.IdConnection });
                    table.ForeignKey(
                        name: "FK_BelongToSchemes_Connections_IdConnection",
                        column: x => x.IdConnection,
                        principalTable: "Connections",
                        principalColumn: "IdConnection",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BelongToSchemes_Schemes_IdScheme",
                        column: x => x.IdScheme,
                        principalTable: "Schemes",
                        principalColumn: "IdScheme");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddedAttributes_IdCharacter",
                table: "AddedAttributes",
                column: "IdCharacter");

            migrationBuilder.CreateIndex(
                name: "IX_BelongToBooks_IdBook",
                table: "BelongToBooks",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_BelongToEvents_IdEvent",
                table: "BelongToEvents",
                column: "IdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_BelongToSchemes_IdConnection",
                table: "BelongToSchemes",
                column: "IdConnection");

            migrationBuilder.CreateIndex(
                name: "IX_BelongToTimelines_IdEvent",
                table: "BelongToTimelines",
                column: "IdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Books_IdPicture",
                table: "Books",
                column: "IdPicture");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_IdBook",
                table: "Characters",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_IdPicture",
                table: "Characters",
                column: "IdPicture");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_IdCharacter1",
                table: "Connections",
                column: "IdCharacter1");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_IdCharacter2",
                table: "Connections",
                column: "IdCharacter2");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_IdCharacter",
                table: "Galleries",
                column: "IdCharacter");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Block",
                table: "Questions",
                column: "Block");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_IdBook",
                table: "Schemes",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_IdBook",
                table: "Timelines",
                column: "IdBook");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddedAttributes");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "BelongToBooks");

            migrationBuilder.DropTable(
                name: "BelongToEvents");

            migrationBuilder.DropTable(
                name: "BelongToSchemes");

            migrationBuilder.DropTable(
                name: "BelongToTimelines");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Sex");

            migrationBuilder.DropTable(
                name: "TypeBelongToBooks");

            migrationBuilder.DropTable(
                name: "TypeConnections");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Timelines");

            migrationBuilder.DropTable(
                name: "NumberBlocks");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Pictures");
        }
    }
}
