using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IBI_SmartHome_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScenesAndClimateSchedul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClimateSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateSchedules", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ClimateSchedules",
                columns: new[] { "Id", "Day", "Mode", "Temp", "Time" },
                values: new object[,]
                {
                    { 1, "Mon-Fri", "Heat", "72°", "07:00 AM" },
                    { 2, "Mon-Fri", "Eco", "68°", "09:00 AM" },
                    { 3, "Mon-Fri", "Heat", "72°", "05:00 PM" },
                    { 4, "Sat-Sun", "Heat", "72°", "08:00 AM" },
                    { 5, "Sat-Sun", "Sleep", "67°", "11:00 PM" }
                });

            migrationBuilder.InsertData(
                table: "Scenes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Arrive Home" },
                    { 2, "Leave Home" },
                    { 3, "Good Morning" },
                    { 4, "Good Night" }
                });

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2026, 2, 18, 10, 48, 23, 742, DateTimeKind.Utc).AddTicks(9046));

            migrationBuilder.InsertData(
                table: "SceneActions",
                columns: new[] { "Id", "DeviceId", "Property", "SceneId", "Value" },
                values: new object[,]
                {
                    { 1, 101, "Power", 1, "true" },
                    { 2, 101, "Power", 2, "false" },
                    { 3, 101, "Power", 3, "true" },
                    { 4, 101, "Brightness", 3, "100" },
                    { 5, 101, "Color", 3, "White" },
                    { 6, 101, "Power", 4, "false" },
                    { 7, 701, "TemperatureValue", 4, "18" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClimateSchedules");

            migrationBuilder.DeleteData(
                table: "SceneActions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SceneActions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SceneActions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SceneActions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SceneActions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SceneActions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SceneActions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2026, 2, 12, 9, 25, 54, 327, DateTimeKind.Utc).AddTicks(5148));
        }
    }
}
