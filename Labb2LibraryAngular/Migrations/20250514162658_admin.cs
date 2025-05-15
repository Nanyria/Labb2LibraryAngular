using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectLibrary.Migrations
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminRole",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1001,
                column: "Timestamp",
                value: new DateTime(2025, 5, 13, 16, 26, 57, 704, DateTimeKind.Utc).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1002,
                column: "Timestamp",
                value: new DateTime(2025, 5, 12, 16, 26, 57, 704, DateTimeKind.Utc).AddTicks(2834));

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1003,
                column: "Timestamp",
                value: new DateTime(2025, 5, 11, 16, 26, 57, 704, DateTimeKind.Utc).AddTicks(2836));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

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
    }
}
