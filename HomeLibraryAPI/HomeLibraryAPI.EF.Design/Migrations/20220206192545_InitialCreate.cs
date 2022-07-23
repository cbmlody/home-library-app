using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibraryAPI.EF.Design.Migrations
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
                name: "Bookshelves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookshelves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
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
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Authors_Books",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Books", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "Author_Books",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Book_Authors",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookshelves_Books",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookshelvesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookshelves_Books", x => new { x.BooksId, x.BookshelvesId });
                    table.ForeignKey(
                        name: "Book_Bookshelves",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Bookshelve_Books",
                        column: x => x.BookshelvesId,
                        principalTable: "Bookshelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { new Guid("08a61211-6bfd-4ffb-9c1b-395f6d1b17c4"), "AuthorName", "Last", "Middle" },
                    { new Guid("dcfad00e-0ea4-458a-b002-9ba54935c52c"), "Author", "Test", null }
                });

            migrationBuilder.InsertData(
                table: "BookSeries",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("216de672-375e-4412-9e5e-4fce850a0d7c"), "Dummy Book series" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "BookSeriesId", "CoverType", "EAN", "Pages", "PublisherId", "Title", "Volume" },
                values: new object[,]
                {
                    { new Guid("042ef9b5-c3e1-45ee-bf40-f1ff39510348"), null, 2, "1212111111111", (short)620, null, "Dummy Book", (short)1 },
                    { new Guid("69c10efb-0fc3-4b87-bb1b-a06263d19d63"), null, 1, "2222222222222", (short)150, null, "Test Book", (short)1 },
                    { new Guid("c80e88ef-455f-42fc-ad9f-0cec12d2363f"), null, 2, "1111111111111", (short)100, null, "Test Book Title", (short)1 },
                    { new Guid("f3a34a7e-fa46-4c25-bbeb-55b5eacd7f93"), null, 2, "1313131313133", (short)400, null, "Test Book Title", (short)2 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("e47b5498-596e-4aa9-aaf5-5778ce5170b8"), "Dummy Publisher" });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Books_BooksId",
                table: "Authors_Books",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookSeriesId",
                table: "Books",
                column: "BookSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookshelves_Books_BookshelvesId",
                table: "Bookshelves_Books",
                column: "BookshelvesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors_Books");

            migrationBuilder.DropTable(
                name: "Bookshelves_Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Bookshelves");

            migrationBuilder.DropTable(
                name: "BookSeries");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
