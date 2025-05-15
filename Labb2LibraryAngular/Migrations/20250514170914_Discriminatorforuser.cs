using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Discriminatorforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1001,
                column: "Timestamp",
                value: new DateTime(2025, 5, 13, 17, 9, 14, 282, DateTimeKind.Utc).AddTicks(2723));

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1002,
                column: "Timestamp",
                value: new DateTime(2025, 5, 12, 17, 9, 14, 282, DateTimeKind.Utc).AddTicks(2726));

            migrationBuilder.UpdateData(
                table: "StatusHistoryItems",
                keyColumn: "StatusHistoryItemID",
                keyValue: 1003,
                column: "Timestamp",
                value: new DateTime(2025, 5, 11, 17, 9, 14, 282, DateTimeKind.Utc).AddTicks(2727));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
