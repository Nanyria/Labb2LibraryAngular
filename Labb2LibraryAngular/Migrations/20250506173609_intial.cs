using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProjectLibrary.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
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
                    Genre = table.Column<int>(type: "int", maxLength: 25, nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    BookDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "StatusHistoryItems",
                columns: table => new
                {
                    StatusHistoryItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    BookStatus = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusHistoryItems", x => x.StatusHistoryItemID);
                    table.ForeignKey(
                        name: "FK_StatusHistoryItems_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusHistoryItems_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Author", "BookDescription", "BookStatus", "Genre", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 101, "F. Scott Fitzgerald", "Lorem Ipsum", 0, 8, 1925, "The Great Gatsby" },
                    { 102, "Harper Lee", "Lorem Ipsum", 0, 8, 1960, "To Kill a Mockingbird" },
                    { 103, "George Orwell", "Lorem Ipsum", 0, 8, 1949, "1984" }
                });

            migrationBuilder.InsertData(
                table: "StatusHistoryItems",
                columns: new[] { "StatusHistoryItemID", "BookID", "BookStatus", "Notes", "Timestamp", "UserID" },
                values: new object[,]
                {
                    { 1, 101, 0, "Initial status", new DateTime(2025, 5, 5, 17, 36, 8, 938, DateTimeKind.Utc).AddTicks(7385), null },
                    { 2, 102, 2, "Initial status", new DateTime(2025, 5, 4, 17, 36, 8, 938, DateTimeKind.Utc).AddTicks(7389), null },
                    { 3, 103, 1, "Initial status", new DateTime(2025, 5, 3, 17, 36, 8, 938, DateTimeKind.Utc).AddTicks(7390), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusHistoryItems_BookID",
                table: "StatusHistoryItems",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusHistoryItems_UserID",
                table: "StatusHistoryItems",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusHistoryItems");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
