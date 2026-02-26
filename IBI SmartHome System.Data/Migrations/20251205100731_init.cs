using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IBI_SmartHome_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MqttTopic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lamp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    IsOn = table.Column<bool>(type: "bit", nullable: false),
                    Brightness = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lamp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lamp_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotionSensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    IsMotionDetected = table.Column<bool>(type: "bit", nullable: false),
                    SensitivityLevel = table.Column<int>(type: "int", nullable: false),
                    BatteryLevel = table.Column<double>(type: "float", nullable: false),
                    LastMotionDetected = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotionSensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotionSensor_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MqttMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MqttMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MqttMessage_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temperature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    TemperatureValue = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperature_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Floor", "Name" },
                values: new object[,]
                {
                    { 11, "First", "Living Room/Kitchen" },
                    { 12, "First", "Guest Bedroom" },
                    { 13, "First", "Utility" },
                    { 14, "First", "Bathroom" },
                    { 15, "First", "Hallway" },
                    { 16, "First", "Mudroom" },
                    { 21, "Second", "Master Bedroom" },
                    { 22, "Second", "Master Bathroom" },
                    { 23, "Second", "Ivan Bedroom" },
                    { 24, "Second", "Ivan Bathroom" },
                    { 25, "Second", "Neli Bedroom" },
                    { 26, "Second", "Neli Bathroom" },
                    { 27, "Second", "Hallway" },
                    { 28, "Second", "Office" },
                    { 99, "Ground", "Outdoor" }
                });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "Id", "MqttTopic", "Name", "RoomId", "Type" },
                values: new object[,]
                {
                    { 101, "telemetry/lamp/livingroom", "Living Room Lamp", 11, 1 },
                    { 401, "telemetry/motionsensor/livingroom", "Living Room Motion Sensor", 11, 3 },
                    { 701, "telemetry/tempsensor/livingroom", "Living Room Temperature Sensor", 11, 2 }
                });

            migrationBuilder.InsertData(
                table: "Lamp",
                columns: new[] { "Id", "Brightness", "Color", "DeviceId", "IsOn" },
                values: new object[] { 1, 75, 1, 101, false });

            migrationBuilder.InsertData(
                table: "MotionSensor",
                columns: new[] { "Id", "BatteryLevel", "DeviceId", "IsMotionDetected", "LastMotionDetected", "SensitivityLevel" },
                values: new object[] { 1, 100.0, 401, false, null, 7 });

            migrationBuilder.InsertData(
                table: "Temperature",
                columns: new[] { "Id", "DeviceId", "Humidity", "TemperatureValue", "Timestamp" },
                values: new object[] { 1, 701, 45, 22.5, new DateTime(2025, 12, 5, 5, 7, 31, 503, DateTimeKind.Utc).AddTicks(1336) });

            migrationBuilder.CreateIndex(
                name: "IX_Device_RoomId",
                table: "Device",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Lamp_DeviceId",
                table: "Lamp",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotionSensor_DeviceId",
                table: "MotionSensor",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MqttMessage_DeviceId",
                table: "MqttMessage",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Temperature_DeviceId",
                table: "Temperature",
                column: "DeviceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lamp");

            migrationBuilder.DropTable(
                name: "MotionSensor");

            migrationBuilder.DropTable(
                name: "MqttMessage");

            migrationBuilder.DropTable(
                name: "Temperature");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
