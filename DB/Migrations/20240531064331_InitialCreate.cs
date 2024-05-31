using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
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
                    idEvent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.idEvent);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    idPicture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.idPicture);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    idBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameBook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idPicture = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.idBook);
                    table.ForeignKey(
                        name: "FK_Books_Pictures_idPicture",
                        column: x => x.idPicture,
                        principalTable: "Pictures",
                        principalColumn: "idPicture");
                });

            migrationBuilder.CreateTable(
                name: "BelongToBooks",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false),
                    idBook = table.Column<int>(type: "int", nullable: false),
                    typeBelong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToBooks", x => new { x.idUser, x.idBook });
                    table.ForeignKey(
                        name: "FK_BelongToBooks_Books_idBook",
                        column: x => x.idBook,
                        principalTable: "Books",
                        principalColumn: "idBook",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BelongToBooks_Users_idUser",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    idCharacter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBook = table.Column<int>(type: "int", nullable: false),
                    idPicture = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.idCharacter);
                    table.ForeignKey(
                        name: "FK_Characters_Books_idBook",
                        column: x => x.idBook,
                        principalTable: "Books",
                        principalColumn: "idBook",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Pictures_idPicture",
                        column: x => x.idPicture,
                        principalTable: "Pictures",
                        principalColumn: "idPicture");
                });

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    idScheme = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameScheme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idBook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.idScheme);
                    table.ForeignKey(
                        name: "FK_Schemes_Books_idBook",
                        column: x => x.idBook,
                        principalTable: "Books",
                        principalColumn: "idBook",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timelines",
                columns: table => new
                {
                    idTimeline = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameTimeline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idBook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timelines", x => x.idTimeline);
                    table.ForeignKey(
                        name: "FK_Timelines_Books_idBook",
                        column: x => x.idBook,
                        principalTable: "Books",
                        principalColumn: "idBook",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddedAttributes",
                columns: table => new
                {
                    idAttribute = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numberBlock = table.Column<int>(type: "int", nullable: false),
                    nameAttribute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contentAttribute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idCharacter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddedAttributes", x => x.idAttribute);
                    table.ForeignKey(
                        name: "FK_AddedAttributes_Characters_idCharacter",
                        column: x => x.idCharacter,
                        principalTable: "Characters",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BelongToEvents",
                columns: table => new
                {
                    idCharacter = table.Column<int>(type: "int", nullable: false),
                    idEvent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToEvents", x => new { x.idCharacter, x.idEvent });
                    table.ForeignKey(
                        name: "FK_BelongToEvents_Characters_idCharacter",
                        column: x => x.idCharacter,
                        principalTable: "Characters",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BelongToEvents_Events_idEvent",
                        column: x => x.idEvent,
                        principalTable: "Events",
                        principalColumn: "idEvent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block1s",
                columns: table => new
                {
                    idCharacter = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question6 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block1s", x => x.idCharacter);
                    table.ForeignKey(
                        name: "FK_Block1s_Characters_idCharacter",
                        column: x => x.idCharacter,
                        principalTable: "Characters",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block2s",
                columns: table => new
                {
                    idCharacter = table.Column<int>(type: "int", nullable: false),
                    question1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question9 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block2s", x => x.idCharacter);
                    table.ForeignKey(
                        name: "FK_Block2s_Characters_idCharacter",
                        column: x => x.idCharacter,
                        principalTable: "Characters",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block3s",
                columns: table => new
                {
                    idCharacter = table.Column<int>(type: "int", nullable: false),
                    question1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question9 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question10 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block3s", x => x.idCharacter);
                    table.ForeignKey(
                        name: "FK_Block3s_Characters_idCharacter",
                        column: x => x.idCharacter,
                        principalTable: "Characters",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block4s",
                columns: table => new
                {
                    idCharacter = table.Column<int>(type: "int", nullable: false),
                    question1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question5 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block4s", x => x.idCharacter);
                    table.ForeignKey(
                        name: "FK_Block4s_Characters_idCharacter",
                        column: x => x.idCharacter,
                        principalTable: "Characters",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    idConnection = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeConnection = table.Column<int>(type: "int", nullable: false),
                    idCharacter1 = table.Column<int>(type: "int", nullable: false),
                    idCharacter2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.idConnection);
                    table.ForeignKey(
                        name: "FK_Connections_Characters_idCharacter1",
                        column: x => x.idCharacter1,
                        principalTable: "Characters",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Characters_idCharacter2",
                        column: x => x.idCharacter2,
                        principalTable: "Characters",
                        principalColumn: "idCharacter");
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    idPicture = table.Column<int>(type: "int", nullable: false),
                    idCharacter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.idPicture);
                    table.ForeignKey(
                        name: "FK_Galleries_Characters_idCharacter",
                        column: x => x.idCharacter,
                        principalTable: "Characters",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Galleries_Pictures_idPicture",
                        column: x => x.idPicture,
                        principalTable: "Pictures",
                        principalColumn: "idPicture",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BelongToTimelines",
                columns: table => new
                {
                    idTimeline = table.Column<int>(type: "int", nullable: false),
                    idEvent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToTimelines", x => new { x.idTimeline, x.idEvent });
                    table.ForeignKey(
                        name: "FK_BelongToTimelines_Events_idEvent",
                        column: x => x.idEvent,
                        principalTable: "Events",
                        principalColumn: "idEvent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BelongToTimelines_Timelines_idTimeline",
                        column: x => x.idTimeline,
                        principalTable: "Timelines",
                        principalColumn: "idTimeline",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BelongToSchemes",
                columns: table => new
                {
                    idScheme = table.Column<int>(type: "int", nullable: false),
                    idConnection = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BelongToSchemes", x => new { x.idScheme, x.idConnection });
                    table.ForeignKey(
                        name: "FK_BelongToSchemes_Connections_idConnection",
                        column: x => x.idConnection,
                        principalTable: "Connections",
                        principalColumn: "idConnection",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BelongToSchemes_Schemes_idScheme",
                        column: x => x.idScheme,
                        principalTable: "Schemes",
                        principalColumn: "idScheme");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddedAttributes_idCharacter",
                table: "AddedAttributes",
                column: "idCharacter");

            migrationBuilder.CreateIndex(
                name: "IX_BelongToBooks_idBook",
                table: "BelongToBooks",
                column: "idBook");

            migrationBuilder.CreateIndex(
                name: "IX_BelongToEvents_idEvent",
                table: "BelongToEvents",
                column: "idEvent");

            migrationBuilder.CreateIndex(
                name: "IX_BelongToSchemes_idConnection",
                table: "BelongToSchemes",
                column: "idConnection");

            migrationBuilder.CreateIndex(
                name: "IX_BelongToTimelines_idEvent",
                table: "BelongToTimelines",
                column: "idEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Books_idPicture",
                table: "Books",
                column: "idPicture");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_idBook",
                table: "Characters",
                column: "idBook");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_idPicture",
                table: "Characters",
                column: "idPicture");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_idCharacter1",
                table: "Connections",
                column: "idCharacter1");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_idCharacter2",
                table: "Connections",
                column: "idCharacter2");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_idCharacter",
                table: "Galleries",
                column: "idCharacter");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_idBook",
                table: "Schemes",
                column: "idBook");

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_idBook",
                table: "Timelines",
                column: "idBook");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddedAttributes");

            migrationBuilder.DropTable(
                name: "BelongToBooks");

            migrationBuilder.DropTable(
                name: "BelongToEvents");

            migrationBuilder.DropTable(
                name: "BelongToSchemes");

            migrationBuilder.DropTable(
                name: "BelongToTimelines");

            migrationBuilder.DropTable(
                name: "Block1s");

            migrationBuilder.DropTable(
                name: "Block2s");

            migrationBuilder.DropTable(
                name: "Block3s");

            migrationBuilder.DropTable(
                name: "Block4s");

            migrationBuilder.DropTable(
                name: "Galleries");

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
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Pictures");
        }
    }
}
