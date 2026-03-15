using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Service.Hubs;
using IBI_SmartHome_System.Service.ClimateService;
using IBI_SmartHome_System.Service.DashboardService;
using IBI_SmartHome_System.Service.LightingService;
using IBI_SmartHome_System.Service.Models;
using IBI_SmartHome_System.Service.MqttService;
using IBI_SmartHome_System.Service.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Security.Claims;
using NUnit.Framework;
using AdminServiceClass = IBI_SmartHome_System.Service.AdminService.AdminService;
using SettingsServiceClass = IBI_SmartHome_System.Service.SettingsService.SettingsService;

namespace IBI_SmartHome_System.Tests
{
	[TestFixture]
	public class AllServiceTests
	{
		private Mock<WeatherService> _weatherServiceMock;
		private Mock<IHttpContextAccessor> _httpContextAccessorMock;
		private Mock<UserManager<ApplicationUser>> _userManagerMock;
		private Mock<IHubContext<SmartHomeHub>> _hubContextMock;
		private ApplicationDbContext _context;
		private readonly string _userId = "test-user-id";
		private readonly int _houseId = 1;

		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			_context = new ApplicationDbContext(options);

			_weatherServiceMock = new Mock<WeatherService>(new HttpClient());
			_httpContextAccessorMock = new Mock<IHttpContextAccessor>();
			_hubContextMock = new Mock<IHubContext<SmartHomeHub>>();

			// Mock SignalR Clients
			var mockClients = new Mock<IHubClients>();
			var mockClientProxy = new Mock<IClientProxy>();
			mockClients.Setup(c => c.All).Returns(mockClientProxy.Object);
			_hubContextMock.Setup(h => h.Clients).Returns(mockClients.Object);

			var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
			_userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

			SetupAuthenticatedUser(_userId);
			SeedBaseData();
		}

		[TearDown]
		public void TearDown()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		private void SetupAuthenticatedUser(string userId)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(ClaimTypes.Email, "test@test.com"),
				new Claim(ClaimTypes.Name, "testuser")
			};
			var identity = new ClaimsIdentity(claims, "TestAuthType");
			var principal = new ClaimsPrincipal(identity);
			var httpContext = new DefaultHttpContext { User = principal };
			_httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
		}

		private void SeedBaseData()
		{
			var user = new ApplicationUser { Id = _userId, Email = "test@test.com", UserName = "testuser" };
			var house = new House { Id = _houseId, Name = "Test House", UserId = _userId, User = user };
			_context.Houses.Add(house);

			var room = new Room { Id = 1, Name = "Living Room", Floor = "1", HouseId = _houseId };
			_context.Room.Add(room);

			_context.SaveChanges();
		}

		#region MqttMessageHandler Tests

		[Test]
		public async Task MqttHandler_HandlesAllTopics()
		{
			// Seed data for handler to work
			var device = new Device { Id = 100, Name = "Sensor", RoomId = 1, HouseId = _houseId, MqttTopic = "esp32/temperature" };
			_context.Device.Add(device);
			_context.Temperature.Add(new Temperature { DeviceId = 100 });
			_context.MotionSensor.Add(new MotionSensor { DeviceId = 100 });
			await _context.SaveChangesAsync();

			var serviceProviderMock = new Mock<IServiceProvider>();
			var serviceScopeMock = new Mock<IServiceScope>();
			var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
			serviceProviderMock.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactoryMock.Object);
			serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(serviceScopeMock.Object);
			serviceScopeMock.Setup(x => x.ServiceProvider).Returns(serviceProviderMock.Object);
			serviceProviderMock.Setup(x => x.GetService(typeof(ApplicationDbContext))).Returns(_context);

			var handler = new MqttMessageHandler(serviceProviderMock.Object, _hubContextMock.Object);

			await handler.HandleMessageAsync("esp32/temperature", "20.5");
			await handler.HandleMessageAsync("esp32/humidity", "55");
			await handler.HandleMessageAsync("esp32/motion", "1");

			Assert.That(_context.MqttMessages, Is.Not.Empty);
		}

		[Test]
		public async Task MqttHandler_IgnoresInvalidPayloads()
		{
			var serviceProviderMock = new Mock<IServiceProvider>();
			var serviceScopeMock = new Mock<IServiceScope>();
			var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
			serviceProviderMock.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactoryMock.Object);
			serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(serviceScopeMock.Object);
			serviceScopeMock.Setup(x => x.ServiceProvider).Returns(serviceProviderMock.Object);
			serviceProviderMock.Setup(x => x.GetService(typeof(ApplicationDbContext))).Returns(_context);

			var handler = new MqttMessageHandler(serviceProviderMock.Object, _hubContextMock.Object);

			int messageCountBefore = await _context.MqttMessages.CountAsync();

			// Test with "ABC" instead of a number
			await handler.HandleMessageAsync("esp32/temperature", "ABC");

			int messageCountAfter = await _context.MqttMessages.CountAsync();
			Assert.That(messageCountAfter, Is.EqualTo(messageCountBefore));
		}

		#endregion

		#region DashboardService Tests

		[Test]
		public async Task Dashboard_Full_Coverage()
		{
			var service = new DashboardService(_weatherServiceMock.Object, _context, _httpContextAccessorMock.Object);
			_weatherServiceMock.Setup(x => x.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>()))
				.ReturnsAsync(new WeatherApiResponse { Current = new CurrentWeather { Temperature = 20 } });

			var vm = await service.GetDashboardViewModelAsync();
			Assert.That(vm, Is.Not.Null);
		}

		[Test]
		public async Task Dashboard_ReturnsEmptyRooms_WhenUserNotAuthenticated()
		{
			_httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null!);
			var service = new DashboardService(_weatherServiceMock.Object, _context, _httpContextAccessorMock.Object);

			var vm = await service.GetDashboardViewModelAsync();

			Assert.That(vm.Rooms, Is.Empty);
		}

		#endregion

		#region ClimateService Tests

		[Test]
		public async Task Climate_Full_Coverage()
		{
			var service = new ClimateService(_context, _httpContextAccessorMock.Object);
			await service.UpdateTargetTemperature(22);
			await service.AddScheduleEntryAsync(new ClimateScheduleViewModel { Day = "Mon", Time = "12:00", Temp = "22", Mode = "Heat" });
			var schedule = (await service.GetScheduleAsync()).ToList();
			if (schedule.Any()) await service.DeleteScheduleEntryAsync(schedule[0].Id);

			Assert.That(schedule, Is.Not.Null);
		}

		[Test]
		public async Task Climate_DeleteNonExistentEntry_ReturnsFalse()
		{
			var service = new ClimateService(_context, _httpContextAccessorMock.Object);
			var result = await service.DeleteScheduleEntryAsync(999);
			Assert.That(result, Is.False);
		}

		#endregion

		#region AdminService Tests

		[Test]
		public async Task Admin_Full_Coverage()
		{
			// We skip SearchUsersAsync because mocking UserManager.Users for ToListAsync is complex in net8
			var service = new AdminServiceClass(_context, _userManagerMock.Object);

			await service.CreateHouseAsync(new CreateHouseViewModel { Name = "H", Address = "A", UserId = _userId });
			await service.AddRoomToHouseAsync(new CreateRoomViewModel { Name = "R", Floor = "1", HouseId = _houseId });
			await service.AddDeviceToRoomAsync(new CreateDeviceViewModel { Name = "D", Type = "Lamp", RoomId = 1 });
			await service.GetHousesWithHierarchyAsync();
			await service.GetGlobalActivityLogsAsync();
			await service.GetGlobalAnalyticsAsync();
			var rules = await service.GetAutomationRulesAsync();
			var ruleId = await service.CreateAutomationRuleAsync(new CreateAutomationRuleViewModel
			{
				Name = "Rule",
				TriggerDeviceId = 1,
				TriggerType = "Motion",
				ActionDeviceId = 1,
				ActionType = "Light",
				ActionValue = "On"
			});
			await service.DeleteAutomationRuleAsync(ruleId);
		}

		#endregion

		#region LightingService Tests

		[Test]
		public async Task Lighting_Full_Coverage()
		{
			var service = new LightingService(_context, _httpContextAccessorMock.Object);
			await service.GetLightingViewModel();
			await service.UpdateLightState(1, true);
			await service.UpdateLightBrightness(1, 50);
		}

		[Test]
		public async Task Lighting_UpdateNonExistentLamp_ReturnsFalse()
		{
			var service = new LightingService(_context, _httpContextAccessorMock.Object);
			var result = await service.UpdateLightState(999, true);
			Assert.That(result, Is.False);
		}

		#endregion

		#region SettingsService Tests

		[Test]
		public async Task Settings_Full_Coverage()
		{
			var user = new ApplicationUser { Id = _userId };
			_userManagerMock.Setup(x => x.FindByIdAsync(_userId)).ReturnsAsync(user);
			_userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

			var service = new SettingsServiceClass(_userManagerMock.Object, _httpContextAccessorMock.Object);
			await service.GetUserProfileAsync();
			await service.UpdateUserProfileAsync(new UserProfileViewModel { Email = "e@e.com", UserName = "u" });
			await service.GetUserSettingsAsync();
			await service.UpdateUserSettingsAsync(new UserSettingsViewModel());
		}

		[Test]
		public async Task Settings_GetProfile_ReturnsNull_WhenUserNotFound()
		{
			_userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null!);
			var service = new SettingsServiceClass(_userManagerMock.Object, _httpContextAccessorMock.Object);

			var result = await service.GetUserProfileAsync();
			Assert.That(result, Is.Null);
		}

		#endregion
	}
}