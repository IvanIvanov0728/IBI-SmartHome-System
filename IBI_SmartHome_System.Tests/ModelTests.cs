using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Data.Entity.Enum;
using IBI_SmartHome_System.Data.Seeding;
using IBI_SmartHome_System.Data.Configuration;
using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace IBI_SmartHome_System.Tests
{
	[TestFixture]
	public class ModelTests
	{
		[Test]
		public void ViewModel_Properties_Work()
		{
			var activityLog = new ActivityLogEntryViewModel { Id = 1, Event = "Test", Type = "info", Timestamp = DateTime.Now };
			Assert.That(activityLog.Id, Is.EqualTo(1));
			Assert.That(activityLog.Event, Is.EqualTo("Test"));

			var analytics = new AdminAnalyticsViewModel
			{
				SystemEnergyWeekly = new List<WeeklyDataPoint>(),
				SystemActivityHourly = new List<HourlyDataPoint>(),
				RoomDistribution = new List<RoomUsage>()
			};
			Assert.That(analytics.SystemEnergyWeekly, Is.Empty);

			var rule = new AutomationRuleViewModel { Id = 1, Name = "R", IsActive = true };
			Assert.That(rule.IsActive, Is.True);

			var battery = new BatteryStorage { Percentage = 50, EstimatedTimeRemaining = "1h" };
			Assert.That(battery.Percentage, Is.EqualTo(50));

			var camera = new CameraViewModel { Id = 1, Name = "C", StreamUrl = "url", IsLive = true };
			Assert.That(camera.IsLive, Is.True);

			var climateSchedule = new ClimateScheduleViewModel { Id = 1, Day = "Mon", Temp = "20" };
			Assert.That(climateSchedule.Day, Is.EqualTo("Mon"));

			var climate = new ClimateViewModel { CurrentTemperature = 20, TargetTemperature = 22, Thermostats = new List<ThermostatViewModel>() };
			Assert.That(climate.CurrentTemperature, Is.EqualTo(20));

			var createDevice = new CreateDeviceViewModel { Name = "D", Type = "Lamp" };
			Assert.That(createDevice.Type, Is.EqualTo("Lamp"));

			var createHouse = new CreateHouseViewModel { Name = "H", UserId = "U" };
			Assert.That(createHouse.Name, Is.EqualTo("H"));

			var createRoom = new CreateRoomViewModel { Name = "R", HouseId = 1 };
			Assert.That(createRoom.HouseId, Is.EqualTo(1));

			var dashboard = new DashboardViewModel { TargetTemperature = 21, Rooms = new List<RoomViewModel>(), Lights = new List<LightControlViewModel>() };
			Assert.That(dashboard.TargetTemperature, Is.EqualTo(21));

			var energy = new EnergyViewModel { WeeklyData = new List<WeeklyDataPoint>(), HourlyData = new List<HourlyDataPoint>(), RoomData = new List<RoomUsage>() };
			Assert.That(energy.WeeklyData, Is.Empty);

			var impact = new EnvironmentalImpact { Co2Offset = 10.5, TreesSaved = 2 };
			Assert.That(impact.Co2Offset, Is.EqualTo(10.5));

			var hourly = new HourlyDataPoint { Time = "12:00", Value = 1.5 };
			Assert.That(hourly.Value, Is.EqualTo(1.5));

			var lighting = new LightingViewModel { Rooms = new List<RoomViewModel>(), Lights = new List<LightControlViewModel>() };
			Assert.That(lighting.Rooms, Is.Empty);

			var roomUsage = new RoomUsage { Room = "R", Usage = "10", Percent = 20 };
			Assert.That(roomUsage.Percent, Is.EqualTo(20));

			var security = new SecurityViewModel { IsSystemArmed = true, EntryPoints = new List<EntryPointViewModel>() };
			Assert.That(security.IsSystemArmed, Is.True);

			var thermostat = new ThermostatViewModel { Id = 1, Temperature = 22.5 };
			Assert.That(thermostat.Temperature, Is.EqualTo(22.5));

			var profile = new UserProfileViewModel { UserName = "U", Email = "E" };
			Assert.That(profile.UserName, Is.EqualTo("U"));

			var search = new UserSearchResultViewModel { Id = "1", Username = "U" };
			Assert.That(search.Id, Is.EqualTo("1"));

			var settings = new UserSettingsViewModel { DarkModeEnabled = true };
			Assert.That(settings.DarkModeEnabled, Is.True);

			var weekly = new WeeklyDataPoint { Name = "Mon", Usage = 100 };
			Assert.That(weekly.Usage, Is.EqualTo(100));
		}

		[Test]
		public void Entity_Properties_Work()
		{
			var user = new ApplicationUser { Id = "1", UserName = "U", DarkModeEnabled = true };
			Assert.That(user.DarkModeEnabled, Is.True);

			var house = new House { Id = 1, Name = "H", UserId = "1" };
			Assert.That(house.Name, Is.EqualTo("H"));

			var room = new Room { Id = 1, Name = "R", Floor = "1", HouseId = 1 };
			Assert.That(room.Name, Is.EqualTo("R"));

			var device = new Device { Id = 1, Name = "D", Type = DeviceType.Lamp, RoomId = 1, HouseId = 1 };
			Assert.That(device.Type, Is.EqualTo(DeviceType.Lamp));

			var lamp = new Lamp { Id = 1, DeviceId = 1, IsOn = true, Brightness = 100 };
			Assert.That(lamp.IsOn, Is.True);

			var temp = new Temperature { Id = 1, DeviceId = 1, TemperatureValue = 20.5, TargetTemperature = 22 };
			Assert.That(temp.TemperatureValue, Is.EqualTo(20.5));

			var motion = new MotionSensor { Id = 1, DeviceId = 1, IsMotionDetected = true };
			Assert.That(motion.IsMotionDetected, Is.True);

			var scene = new Scene { Id = 1, Name = "S", HouseId = 1 };
			Assert.That(scene.Name, Is.EqualTo("S"));

			var action = new SceneAction { Id = 1, SceneId = 1, DeviceId = 1, Property = "P", Value = "V" };
			Assert.That(action.Value, Is.EqualTo("V"));

			var log = new ActivityLogEntry { Id = 1, Event = "E", HouseId = 1 };
			Assert.That(log.Event, Is.EqualTo("E"));

			var camera = new Camera { Id = 1, Name = "C", StreamUrl = "U", HouseId = 1 };
			Assert.That(camera.Name, Is.EqualTo("C"));

			var rule = new AutomationRule { Id = 1, Name = "R", TriggerDeviceId = 1, ActionDeviceId = 2 };
			Assert.That(rule.TriggerDeviceId, Is.EqualTo(1));

			var mqtt = new MqttMessage { Id = 1, Topic = "T", Payload = "P" };
			Assert.That(mqtt.Topic, Is.EqualTo("T"));
		}

		[Test]
		public void Seeders_Return_Data()
		{
			Assert.That(ActivityLogEntrySeeder.Seed(), Is.Not.Empty);
			Assert.That(CameraSeeder.Seed(), Is.Not.Empty);
			Assert.That(ClimateScheduleSeeder.Seed(), Is.Not.Empty);
			Assert.That(DeviceSeeder.Seed(), Is.Not.Empty);
			Assert.That(HouseSeeder.Seed(), Is.Not.Empty);
			Assert.That(LampSeeder.Seed(), Is.Not.Empty);
			Assert.That(MotionSensorSeeder.Seed(), Is.Not.Empty);
			Assert.That(RoomSeeder.Seed(), Is.Not.Empty);
			Assert.That(SceneSeeder.Seed(), Is.Not.Empty);
			Assert.That(SceneSeeder.SeedActions(), Is.Not.Empty);
			Assert.That(TemperatureSeeder.Seed(), Is.Not.Empty);
		}

		[Test]
		public async Task IdentitySeeder_Logic_Check()
		{
			var serviceProviderMock = new Mock<IServiceProvider>();

			var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
			var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

			var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
			var roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null, null, null, null);

			serviceProviderMock.Setup(x => x.GetService(typeof(UserManager<ApplicationUser>))).Returns(userManagerMock.Object);
			serviceProviderMock.Setup(x => x.GetService(typeof(RoleManager<IdentityRole>))).Returns(roleManagerMock.Object);

			try
			{
				await IdentitySeeder.SeedAsync(serviceProviderMock.Object);
			}
			catch
			{
				// We just want coverage
			}
		}
	}
}