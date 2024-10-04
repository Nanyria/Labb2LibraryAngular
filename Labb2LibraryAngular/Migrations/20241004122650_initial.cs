using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb2LibraryAngular.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    BookDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInStock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Author", "BookDescription", "Genre", "IsInStock", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 101, "F. Scott Fitzgerald", "Lorem Ipsum", "Fiction", true, 1925, "The Great Gatsby" },
                    { 102, "Harper Lee", "Lorem Ipsum", "Fiction", true, 1960, "To Kill a Mockingbird" },
                    { 103, "George Orwell", "Lorem Ipsum", "Fiction", false, 1949, "1984" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
