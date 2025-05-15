using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectLibrary.Migrations
{
    /// <inheritdoc />
    public partial class bookType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookType",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.Sql("UPDATE Books SET BookType = 0 WHERE BookType IS NULL");
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1001,
                columns: new[] { "BookType", "Genre" },
                values: new object[] { 2, 9 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1002,
                columns: new[] { "BookType", "Genre" },
                values: new object[] { 2, 9 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1003,
                columns: new[] { "BookType", "Genre" },
                values: new object[] { 1, 9 });

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1001,
                column: "Timestamp",
                value: new DateTime(2025, 5, 13, 15, 58, 8, 551, DateTimeKind.Utc).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1002,
                column: "Timestamp",
                value: new DateTime(2025, 5, 12, 15, 58, 8, 551, DateTimeKind.Utc).AddTicks(2751));

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1003,
                column: "Timestamp",
                value: new DateTime(2025, 5, 11, 15, 58, 8, 551, DateTimeKind.Utc).AddTicks(2752));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookType",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1001,
                column: "Genre",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1002,
                column: "Genre",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1003,
                column: "Genre",
                value: 8);

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1001,
                column: "Timestamp",
                value: new DateTime(2025, 5, 7, 14, 2, 2, 393, DateTimeKind.Utc).AddTicks(5683));

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1002,
                column: "Timestamp",
                value: new DateTime(2025, 5, 6, 14, 2, 2, 393, DateTimeKind.Utc).AddTicks(5688));

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1003,
                column: "Timestamp",
                value: new DateTime(2025, 5, 5, 14, 2, 2, 393, DateTimeKind.Utc).AddTicks(5690));
        }
    }
}
