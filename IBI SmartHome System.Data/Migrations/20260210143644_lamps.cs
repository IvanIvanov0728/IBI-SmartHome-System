using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IBI_SmartHome_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class lamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lamp_Device_DeviceId",
                table: "Lamp");

            migrationBuilder.DropForeignKey(
                name: "FK_MqttMessage_Device_DeviceId",
                table: "MqttMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MqttMessage",
                table: "MqttMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lamp",
                table: "Lamp");

            migrationBuilder.RenameTable(
                name: "MqttMessage",
                newName: "MqttMessages");

            migrationBuilder.RenameTable(
                name: "Lamp",
                newName: "Lamps");

            migrationBuilder.RenameIndex(
                name: "IX_MqttMessage_DeviceId",
                table: "MqttMessages",
                newName: "IX_MqttMessages_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Lamp_DeviceId",
                table: "Lamps",
                newName: "IX_Lamps_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MqttMessages",
                table: "MqttMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lamps",
                table: "Lamps",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Scenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SceneActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SceneId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SceneActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SceneActions_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SceneActions_Scenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "Scenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2026, 2, 10, 9, 36, 44, 128, DateTimeKind.Utc).AddTicks(7556));

            migrationBuilder.CreateIndex(
                name: "IX_SceneActions_DeviceId",
                table: "SceneActions",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_SceneActions_SceneId",
                table: "SceneActions",
                column: "SceneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lamps_Device_DeviceId",
                table: "Lamps",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MqttMessages_Device_DeviceId",
                table: "MqttMessages",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lamps_Device_DeviceId",
                table: "Lamps");

            migrationBuilder.DropForeignKey(
                name: "FK_MqttMessages_Device_DeviceId",
                table: "MqttMessages");

            migrationBuilder.DropTable(
                name: "SceneActions");

            migrationBuilder.DropTable(
                name: "Scenes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MqttMessages",
                table: "MqttMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lamps",
                table: "Lamps");

            migrationBuilder.RenameTable(
                name: "MqttMessages",
                newName: "MqttMessage");

            migrationBuilder.RenameTable(
                name: "Lamps",
                newName: "Lamp");

            migrationBuilder.RenameIndex(
                name: "IX_MqttMessages_DeviceId",
                table: "MqttMessage",
                newName: "IX_MqttMessage_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Lamps_DeviceId",
                table: "Lamp",
                newName: "IX_Lamp_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MqttMessage",
                table: "MqttMessage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lamp",
                table: "Lamp",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2025, 12, 5, 5, 7, 31, 503, DateTimeKind.Utc).AddTicks(1336));

            migrationBuilder.AddForeignKey(
                name: "FK_Lamp_Device_DeviceId",
                table: "Lamp",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MqttMessage_Device_DeviceId",
                table: "MqttMessage",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
