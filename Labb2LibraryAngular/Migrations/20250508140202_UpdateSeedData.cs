using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProjectLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 103);

            migrationBuilder.AlterColumn<int>(
                name: "StatusHistoryItemID",
                table: "StatusHistoryItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "ReservationItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CheckOutItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Author", "BookDescription", "BookStatus", "Genre", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 1001, "F. Scott Fitzgerald", "Lorem Ipsum", 0, 8, 1925, "The Great Gatsby" },
                    { 1002, "Harper Lee", "Lorem Ipsum", 0, 8, 1960, "To Kill a Mockingbird" },
                    { 1003, "George Orwell", "Lorem Ipsum", 0, 8, 1949, "1984" }
                });

            migrationBuilder.InsertData(
                table: "StatusHistoryItems",
                columns: new[] { "StatusHistoryItemID", "BookID", "BookStatus", "Notes", "Timestamp", "UserID" },
                values: new object[,]
                {
                    { 1001, 1001, 0, "Initial status", new DateTime(2025, 5, 7, 14, 2, 2, 393, DateTimeKind.Utc).AddTicks(5683), null },
                    { 1002, 1002, 2, "Initial status", new DateTime(2025, 5, 6, 14, 2, 2, 393, DateTimeKind.Utc).AddTicks(5688), null },
                    { 1003, 1003, 1, "Initial status", new DateTime(2025, 5, 5, 14, 2, 2, 393, DateTimeKind.Utc).AddTicks(5690), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1003);

            migrationBuilder.AlterColumn<int>(
                name: "StatusHistoryItemID",
                table: "StatusHistoryItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "ReservationItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CheckOutItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

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
                    { 1, 101, 0, "Initial status", new DateTime(2025, 5, 7, 13, 34, 1, 762, DateTimeKind.Utc).AddTicks(1654), null },
                    { 2, 102, 2, "Initial status", new DateTime(2025, 5, 6, 13, 34, 1, 762, DateTimeKind.Utc).AddTicks(1659), null },
                    { 3, 103, 1, "Initial status", new DateTime(2025, 5, 5, 13, 34, 1, 762, DateTimeKind.Utc).AddTicks(1661), null }
                });
        }
    }
}
