using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EAN = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Volume = table.Column<short>(type: "smallint", nullable: false),
                    Pages = table.Column<short>(type: "smallint", nullable: false),
                    CoverType = table.Column<int>(type: "int", nullable: false),
                    BookSeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookSeries_BookSeriesId",
                        column: x => x.BookSeriesId,
                        principalTable: "BookSeries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthorsBooks",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsBooks", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "AuthorBooks",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "BookAuthors",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsBooks_BooksId",
                table: "AuthorsBooks",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookSeriesId",
                table: "Books",
                column: "BookSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorsBooks");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BookSeries");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
