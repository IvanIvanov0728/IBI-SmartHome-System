using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IBI_SmartHome_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Camerafeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2026, 3, 19, 7, 59, 29, 984, DateTimeKind.Utc).AddTicks(9629));

            migrationBuilder.UpdateData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2026, 3, 19, 7, 49, 29, 984, DateTimeKind.Utc).AddTicks(9633));

            migrationBuilder.UpdateData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2026, 3, 19, 7, 34, 29, 984, DateTimeKind.Utc).AddTicks(9635));

            migrationBuilder.UpdateData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2026, 3, 19, 7, 4, 29, 984, DateTimeKind.Utc).AddTicks(9636));

            migrationBuilder.UpdateData(
                table: "Cameras",
                keyColumn: "Id",
                keyValue: 1,
                column: "StreamUrl",
                value: "http://localhost:8080/video");

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2026, 3, 19, 3, 4, 29, 984, DateTimeKind.Utc).AddTicks(9234));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2026, 3, 2, 14, 34, 29, 181, DateTimeKind.Utc).AddTicks(6719));

            migrationBuilder.UpdateData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2026, 3, 2, 14, 24, 29, 181, DateTimeKind.Utc).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Timestamp",
                value: new DateTime(2026, 3, 2, 14, 9, 29, 181, DateTimeKind.Utc).AddTicks(6725));

            migrationBuilder.UpdateData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 4,
                column: "Timestamp",
                value: new DateTime(2026, 3, 2, 13, 39, 29, 181, DateTimeKind.Utc).AddTicks(6727));

            migrationBuilder.UpdateData(
                table: "Cameras",
                keyColumn: "Id",
                keyValue: 1,
                column: "StreamUrl",
                value: "https://example.com/stream/frontporch");

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2026, 3, 2, 9, 39, 29, 181, DateTimeKind.Utc).AddTicks(6265));
        }
    }
}
