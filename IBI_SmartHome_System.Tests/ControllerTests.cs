using IBI_SmartHome_System.Controllers;
using IBI_SmartHome_System.Service.AdminService;
using IBI_SmartHome_System.Service.ClimateService;
using IBI_SmartHome_System.Service.DashboardService;
using IBI_SmartHome_System.Service.EnergyService;
using IBI_SmartHome_System.Service.LightingService;
using IBI_SmartHome_System.Service.Models;
using IBI_SmartHome_System.Service.SceneService;
using IBI_SmartHome_System.Service.SecurityService;
using IBI_SmartHome_System.Service.SettingsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace IBI_SmartHome_System.Tests
{
	[TestFixture]
	public class ControllerTests
	{
		[Test]
		public async Task DashboardController_GetDashboard_ReturnsOk()
		{
			var mockService = new Mock<IDashboardService>();
			mockService.Setup(s => s.GetDashboardViewModelAsync()).ReturnsAsync(new IBI_SmartHome_System.Service.Models.DashboardViewModel());
			var controller = new DashboardController(mockService.Object);

			var result = await controller.GetDashboard();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
			var okResult = (OkObjectResult)result;
			Assert.That(okResult.Value, Is.InstanceOf<IBI_SmartHome_System.Service.Models.DashboardViewModel>());
		}

		[Test]
		public async Task DashboardController_GetDashboard_ReturnsNotFound_WhenDataIsNull()
		{
			// Arrange
			var mockService = new Mock<IDashboardService>();
			mockService.Setup(s => s.GetDashboardViewModelAsync()).ReturnsAsync((IBI_SmartHome_System.Service.Models.DashboardViewModel)null!);
			var controller = new DashboardController(mockService.Object);

			// Act
			var result = await controller.GetDashboard();

			// Assert
			Assert.That(result, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public async Task ClimateController_GetStatus_ReturnsOk()
		{
			var mockService = new Mock<IClimateService>();
			mockService.Setup(s => s.GetClimateViewModelAsync()).ReturnsAsync(new ClimateViewModel());
			var controller = new ClimateController(mockService.Object);

			var result = await controller.GetClimateStatus();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task ClimateController_GetStatus_ReturnsNotFound_WhenDataIsNull()
		{
			var mockService = new Mock<IClimateService>();
			mockService.Setup(s => s.GetClimateViewModelAsync()).ReturnsAsync((ClimateViewModel)null!);
			var controller = new ClimateController(mockService.Object);

			var result = await controller.GetClimateStatus();

			Assert.That(result, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public async Task ClimateController_UpdateTemperature_ReturnsOk()
		{
			var mockService = new Mock<IClimateService>();
			var controller = new ClimateController(mockService.Object);
			var request = new UpdateTemperatureRequest { TargetTemperature = 25 };

			var result = await controller.UpdateTemperature(request);

			Assert.That(result, Is.InstanceOf<OkResult>());
			mockService.Verify(s => s.UpdateTargetTemperature(25), Times.Once);
		}

		[Test]
		public async Task ClimateController_UpdateTemperature_ReturnsBadRequest_WhenRequestIsNull()
		{
			var mockService = new Mock<IClimateService>();
			var controller = new ClimateController(mockService.Object);

			var result = await controller.UpdateTemperature(null!);

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}

		[Test]
		public async Task LightingController_GetLights_ReturnsOk()
		{
			var mockService = new Mock<ILightingService>();
			mockService.Setup(s => s.GetLightingViewModel()).ReturnsAsync(new IBI_SmartHome_System.Service.Models.LightingViewModel());
			var controller = new LightingController(mockService.Object);

			var result = await controller.GetLights();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task LightingController_GetLights_ReturnsNotFound_WhenDataIsNull()
		{
			var mockService = new Mock<ILightingService>();
			mockService.Setup(s => s.GetLightingViewModel()).ReturnsAsync((IBI_SmartHome_System.Service.Models.LightingViewModel)null!);
			var controller = new LightingController(mockService.Object);

			var result = await controller.GetLights();

			Assert.That(result, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public async Task ScenesController_Execute_ReturnsOk()
		{
			var mockService = new Mock<ISceneService>();
			mockService.Setup(s => s.ExecuteSceneAsync(1)).ReturnsAsync(true);
			var controller = new ScenesController(mockService.Object);

			var result = await controller.Execute(1);

			Assert.That(result, Is.InstanceOf<OkResult>());
			mockService.Verify(s => s.ExecuteSceneAsync(1), Times.Once);
		}

		[Test]
		public async Task ScenesController_Execute_ReturnsNotFound_WhenSceneDoesNotExist()
		{
			var mockService = new Mock<ISceneService>();
			mockService.Setup(s => s.ExecuteSceneAsync(999)).ReturnsAsync(false);
			var controller = new ScenesController(mockService.Object);

			var result = await controller.Execute(999);

			Assert.That(result, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public async Task SecurityController_GetOverview_ReturnsOk()
		{
			var mockService = new Mock<ISecurityService>();
			mockService.Setup(s => s.GetSecurityOverviewAsync()).ReturnsAsync(new SecurityViewModel());
			var controller = new SecurityController(mockService.Object);

			var result = await controller.GetSecurityOverview();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task SecurityController_GetOverview_ReturnsNotFound_WhenDataIsNull()
		{
			var mockService = new Mock<ISecurityService>();
			mockService.Setup(s => s.GetSecurityOverviewAsync()).ReturnsAsync((SecurityViewModel)null!);
			var controller = new SecurityController(mockService.Object);

			var result = await controller.GetSecurityOverview();

			Assert.That(result, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public async Task SecurityController_UpdateEntryPointStatus_ReturnsOk()
		{
			var mockService = new Mock<ISecurityService>();
			mockService.Setup(s => s.UpdateEntryPointLockStatus(1, true)).ReturnsAsync(true);
			var controller = new SecurityController(mockService.Object);

			var result = await controller.UpdateEntryPointStatus(1, true);

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task SecurityController_UpdateEntryPointStatus_ReturnsBadRequest_WhenUpdateFails()
		{
			var mockService = new Mock<ISecurityService>();
			mockService.Setup(s => s.UpdateEntryPointLockStatus(999, true)).ReturnsAsync(false);
			var controller = new SecurityController(mockService.Object);

			var result = await controller.UpdateEntryPointStatus(999, true);

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}

		[Test]
		public async Task AdminController_GetHierarchy_ReturnsOk()
		{
			var mockService = new Mock<IAdminService>();
			mockService.Setup(s => s.GetHousesWithHierarchyAsync()).ReturnsAsync(new List<HouseHierarchyViewModel>());
			var controller = new AdminController(mockService.Object);

			var result = await controller.GetHierarchy();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task AdminController_GetHierarchy_ReturnsNotFound_WhenDataIsNull()
		{
			var mockService = new Mock<IAdminService>();
			mockService.Setup(s => s.GetHousesWithHierarchyAsync()).ReturnsAsync((List<HouseHierarchyViewModel>)null!);
			var controller = new AdminController(mockService.Object);

			var result = await controller.GetHierarchy();

			Assert.That(result, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public async Task AdminController_CreateHouse_ReturnsOk()
		{
			var mockService = new Mock<IAdminService>();
			mockService.Setup(s => s.CreateHouseAsync(It.IsAny<CreateHouseViewModel>())).ReturnsAsync(1);
			var controller = new AdminController(mockService.Object);

			var result = await controller.CreateHouse(new CreateHouseViewModel { Name = "H", UserId = "U" });

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task AdminController_AddRoom_ReturnsOk()
		{
			var mockService = new Mock<IAdminService>();
			mockService.Setup(s => s.AddRoomToHouseAsync(It.IsAny<CreateRoomViewModel>())).ReturnsAsync(1);
			var controller = new AdminController(mockService.Object);

			var result = await controller.AddRoom(new CreateRoomViewModel { Name = "R", HouseId = 1 });

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task AdminController_AddDevice_ReturnsOk()
		{
			var mockService = new Mock<IAdminService>();
			mockService.Setup(s => s.AddDeviceToRoomAsync(It.IsAny<CreateDeviceViewModel>())).ReturnsAsync(1);
			var controller = new AdminController(mockService.Object);

			var result = await controller.AddDevice(new CreateDeviceViewModel { Name = "D", RoomId = 1 });

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task EnergyController_Get_ReturnsOk()
		{
			var mockService = new Mock<IEnergyService>();
			mockService.Setup(s => s.GetEnergyDataAsync()).ReturnsAsync(new EnergyViewModel());
			var controller = new EnergyController(mockService.Object);

			var result = await controller.Get();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task EnergyController_Get_ReturnsNotFound_WhenDataIsNull()
		{
			var mockService = new Mock<IEnergyService>();
			mockService.Setup(s => s.GetEnergyDataAsync()).ReturnsAsync((EnergyViewModel)null!);
			var controller = new EnergyController(mockService.Object);

			var result = await controller.Get();

			Assert.That(result, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public async Task SettingsController_GetProfile_ReturnsOk()
		{
			var mockService = new Mock<ISettingsService>();
			mockService.Setup(s => s.GetUserProfileAsync()).ReturnsAsync(new UserProfileViewModel());
			var controller = new SettingsController(mockService.Object);

			var result = await controller.GetUserProfile();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task SettingsController_GetProfile_ReturnsNotFound_WhenDataIsNull()
		{
			var mockService = new Mock<ISettingsService>();
			mockService.Setup(s => s.GetUserProfileAsync()).ReturnsAsync((UserProfileViewModel)null!);
			var controller = new SettingsController(mockService.Object);

			var result = await controller.GetUserProfile();

			Assert.That(result, Is.InstanceOf<NotFoundResult>());
		}

		[Test]
		public async Task SettingsController_UpdateUserSettings_ReturnsOk()
		{
			var mockService = new Mock<ISettingsService>();
			mockService.Setup(s => s.UpdateUserSettingsAsync(It.IsAny<UserSettingsViewModel>())).ReturnsAsync(true);
			var controller = new SettingsController(mockService.Object);

			var result = await controller.UpdateUserSettings(new UserSettingsViewModel());

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task SettingsController_UpdateUserSettings_ReturnsBadRequest_WhenUpdateFails()
		{
			var mockService = new Mock<ISettingsService>();
			mockService.Setup(s => s.UpdateUserSettingsAsync(It.IsAny<UserSettingsViewModel>())).ReturnsAsync(false);
			var controller = new SettingsController(mockService.Object);

			var result = await controller.UpdateUserSettings(new UserSettingsViewModel());

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}

		[Test]
		public void HomeController_Index_ReturnsView()
		{
			var mockLogger = new Mock<ILogger<HomeController>>();
			var controller = new HomeController(mockLogger.Object);

			var result = controller.Index();

			Assert.That(result, Is.InstanceOf<ViewResult>());
		}
	}
}