using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IBI_SmartHome_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReceiveNotifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DarkModeEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserRole = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<double>(type: "double", nullable: true),
                    Longitude = table.Column<double>(type: "double", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StreamUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsLive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cameras_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClimateSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Temp = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimateSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClimateSchedules_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Floor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Scenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scenes_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MqttTopic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    IsLocked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDoor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsWindow = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActivityLogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Event = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogEntries_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivityLogEntries_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutomationRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TriggerDeviceId = table.Column<int>(type: "int", nullable: false),
                    TriggerType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConditionValue = table.Column<double>(type: "double", nullable: true),
                    ActionDeviceId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActionValue = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomationRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutomationRules_Device_ActionDeviceId",
                        column: x => x.ActionDeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AutomationRules_Device_TriggerDeviceId",
                        column: x => x.TriggerDeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lamps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    IsOn = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Brightness = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lamps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lamps_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MotionSensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    IsMotionDetected = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SensitivityLevel = table.Column<int>(type: "int", nullable: false),
                    BatteryLevel = table.Column<double>(type: "double", nullable: false),
                    LastMotionDetected = table.Column<DateTime>(type: "datetime(6)", nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MqttMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Topic = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Payload = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReceivedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MqttMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MqttMessages_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SceneActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SceneId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Property = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Temperature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    TemperatureValue = table.Column<double>(type: "double", nullable: false),
                    TargetTemperature = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DarkModeEnabled", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ReceiveNotifications", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserRole" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "64882ab4-569f-4f46-aa39-ce6c2acee221", false, "admin@smarthome.com", true, false, null, "ADMIN@SMARTHOME.COM", "ADMIN@SMARTHOME.COM", null, null, false, true, "0db128b1-bcbf-4097-b24b-53b212a133a9", false, "admin@smarthome.com", "Admin" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "Latitude", "Longitude", "Name", "UserId" },
                values: new object[] { 1, "123 Smart Home Lane, Sofia", 42.700000000000003, 23.32, "Admin's Home", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "ActivityLogEntries",
                columns: new[] { "Id", "DeviceId", "Event", "HouseId", "Timestamp", "Type" },
                values: new object[,]
                {
                    { 2, null, "Motion Detected at Front Porch", 1, new DateTime(2026, 3, 23, 18, 26, 53, 431, DateTimeKind.Utc).AddTicks(274), "warning" },
                    { 4, null, "Admin Logged In", 1, new DateTime(2026, 3, 23, 17, 41, 53, 431, DateTimeKind.Utc).AddTicks(276), "success" }
                });

            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "Id", "HouseId", "IsLive", "Name", "StreamUrl" },
                values: new object[,]
                {
                    { 1, 1, true, "Front Porch Camera", "http://localhost:8080/video" },
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
                    { 1, 801, "Front Door Locked", 1, new DateTime(2026, 3, 23, 18, 36, 53, 431, DateTimeKind.Utc).AddTicks(267), "info" },
                    { 3, 802, "Back Door Unlocked", 1, new DateTime(2026, 3, 23, 18, 11, 53, 431, DateTimeKind.Utc).AddTicks(275), "info" }
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
                values: new object[] { 1, 701, 45, 0, 22.5, new DateTime(2026, 3, 23, 13, 41, 53, 430, DateTimeKind.Utc).AddTicks(9915) });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogEntries_DeviceId",
                table: "ActivityLogEntries",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogEntries_HouseId",
                table: "ActivityLogEntries",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutomationRules_ActionDeviceId",
                table: "AutomationRules",
                column: "ActionDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_AutomationRules_TriggerDeviceId",
                table: "AutomationRules",
                column: "TriggerDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_HouseId",
                table: "Cameras",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClimateSchedules_HouseId",
                table: "ClimateSchedules",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_HouseId",
                table: "Device",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_RoomId",
                table: "Device",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_UserId",
                table: "Houses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lamps_DeviceId",
                table: "Lamps",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotionSensor_DeviceId",
                table: "MotionSensor",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MqttMessages_DeviceId",
                table: "MqttMessages",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_HouseId",
                table: "Room",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SceneActions_DeviceId",
                table: "SceneActions",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_SceneActions_SceneId",
                table: "SceneActions",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_HouseId",
                table: "Scenes",
                column: "HouseId");

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
                name: "ActivityLogEntries");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AutomationRules");

            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "ClimateSchedules");

            migrationBuilder.DropTable(
                name: "Lamps");

            migrationBuilder.DropTable(
                name: "MotionSensor");

            migrationBuilder.DropTable(
                name: "MqttMessages");

            migrationBuilder.DropTable(
                name: "SceneActions");

            migrationBuilder.DropTable(
                name: "Temperature");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Scenes");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
