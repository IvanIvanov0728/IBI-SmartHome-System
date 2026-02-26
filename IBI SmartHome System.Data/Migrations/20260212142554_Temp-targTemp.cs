using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IBI_SmartHome_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class TemptargTemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetTemperature",
                table: "Temperature",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TargetTemperature", "Timestamp" },
                values: new object[] { 28, new DateTime(2026, 2, 12, 9, 25, 54, 327, DateTimeKind.Utc).AddTicks(5148) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetTemperature",
                table: "Temperature");

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2026, 2, 10, 9, 36, 44, 128, DateTimeKind.Utc).AddTicks(7556));
        }
    }
}
