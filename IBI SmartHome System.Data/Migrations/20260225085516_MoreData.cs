using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IBI_SmartHome_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "Latitude", "Longitude", "Name", "UserId" },
                values: new object[] { 1, "123 Smart Home Lane, Sofia", 42.700000000000003, 23.32, "Admin's Home", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "ActivityLogEntries",
                columns: new[] { "Id", "DeviceId", "Event", "HouseId", "Timestamp", "Type" },
                values: new object[,]
                {
                    { 2, null, "Motion Detected at Front Porch", 1, new DateTime(2026, 2, 25, 8, 40, 15, 838, DateTimeKind.Utc).AddTicks(3014), "warning" },
                    { 4, null, "Admin Logged In", 1, new DateTime(2026, 2, 25, 7, 55, 15, 838, DateTimeKind.Utc).AddTicks(3017), "success" }
                });

            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "Id", "HouseId", "IsLive", "Name", "StreamUrl" },
                values: new object[,]
                {
                    { 1, 1, true, "Front Porch Camera", "https://example.com/stream/frontporch" },
                    { 2, 1, false, "Backyard Camera", "https://example.com/stream/backyard" }
                });

            migrationBuilder.InsertData(
                table: "ClimateSchedules",
                columns: new[] { "Id", "Day", "HouseId", "Mode", "Temp", "Time" },
                values: new object[,]
                {
                    { 1, "Mon-Fri", 1, "Heat", "72°", "07:00 AM" },
                    { 2, "Mon-Fri", 1, "Eco", "68°", "09:00 AM" },
                    { 3, "Mon-Fri", 1, "Heat", "72°", "05:00 PM" },
                    { 4, "Sat-Sun", 1, "Heat", "72°", "08:00 AM" },
                    { 5, "Sat-Sun", 1, "Sleep", "67°", "11:00 PM" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Floor", "HouseId", "Name" },
                values: new object[,]
                {
                    { 11, "First", 1, "Living Room/Kitchen" },
                    { 12, "First", 1, "Guest Bedroom" },
                    { 13, "First", 1, "Utility" },
                    { 14, "First", 1, "Bathroom" },
                    { 15, "First", 1, "Hallway" },
                    { 16, "First", 1, "Mudroom" },
                    { 21, "Second", 1, "Master Bedroom" },
                    { 22, "Second", 1, "Master Bathroom" },
                    { 23, "Second", 1, "Ivan Bedroom" },
                    { 24, "Second", 1, "Ivan Bathroom" },
                    { 25, "Second", 1, "Neli Bedroom" },
                    { 26, "Second", 1, "Neli Bathroom" },
                    { 27, "Second", 1, "Hallway" },
                    { 28, "Second", 1, "Office" },
                    { 99, "Ground", 1, "Outdoor" }
                });

            migrationBuilder.InsertData(
                table: "Scenes",
                columns: new[] { "Id", "HouseId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Arrive Home" },
                    { 2, 1, "Leave Home" },
                    { 3, 1, "Good Morning" },
                    { 4, 1, "Good Night" }
                });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "Id", "HouseId", "IsDoor", "IsLocked", "IsWindow", "MqttTopic", "Name", "RoomId", "Type" },
                values: new object[,]
                {
                    { 101, 1, false, false, false, "telemetry/lamp/livingroom", "Living Room Lamp", 11, 1 },
                    { 401, 1, false, false, false, "telemetry/motionsensor/livingroom", "Living Room Motion Sensor", 11, 3 },
                    { 701, 1, false, false, false, "telemetry/tempsensor/livingroom", "Living Room Temperature Sensor", 11, 2 },
                    { 801, 1, true, true, false, "security/door/front", "Front Door", 11, 4 },
                    { 802, 1, true, false, false, "security/door/back", "Back Door", 11, 4 },
                    { 803, 1, false, true, true, "security/window/guest", "Guest Window", 12, 4 }
                });

            migrationBuilder.InsertData(
                table: "ActivityLogEntries",
                columns: new[] { "Id", "DeviceId", "Event", "HouseId", "Timestamp", "Type" },
                values: new object[,]
                {
                    { 1, 801, "Front Door Locked", 1, new DateTime(2026, 2, 25, 8, 50, 15, 838, DateTimeKind.Utc).AddTicks(2993), "info" },
                    { 3, 802, "Back Door Unlocked", 1, new DateTime(2026, 2, 25, 8, 25, 15, 838, DateTimeKind.Utc).AddTicks(3016), "info" }
                });

            migrationBuilder.InsertData(
                table: "Lamps",
                columns: new[] { "Id", "Brightness", "Color", "DeviceId", "IsOn" },
                values: new object[] { 1, 75, 1, 101, false });

            migrationBuilder.InsertData(
                table: "MotionSensor",
                columns: new[] { "Id", "BatteryLevel", "DeviceId", "IsMotionDetected", "LastMotionDetected", "SensitivityLevel" },
                values: new object[] { 1, 100.0, 401, false, null, 7 });

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

            migrationBuilder.InsertData(
                table: "Temperature",
                columns: new[] { "Id", "DeviceId", "Humidity", "TargetTemperature", "TemperatureValue", "Timestamp" },
                values: new object[] { 1, 701, 45, 0, 22.5, new DateTime(2026, 2, 25, 3, 55, 15, 838, DateTimeKind.Utc).AddTicks(2710) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ActivityLogEntries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClimateSchedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClimateSchedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClimateSchedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClimateSchedules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClimateSchedules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Device",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "Lamps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MotionSensor",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 99);

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
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Device",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Device",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Device",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "Device",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "Device",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 12);

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

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
