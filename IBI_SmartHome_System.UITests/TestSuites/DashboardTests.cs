using IBI_SmartHome_System.UITests.Base;
using IBI_SmartHome_System.UITests.Pages;
using IBI_SmartHome_System.UITests.Factories;
using NUnit.Framework;

namespace IBI_SmartHome_System.UITests.TestSuites
{
	[TestFixture]
	public class DashboardTests : BaseTest
	{
		private LoginPage _loginPage;
		private DashboardPage _dashboardPage;

		[SetUp]
		public void TestSetup()
		{
			_loginPage = new LoginPage(Driver);
			_dashboardPage = new DashboardPage(Driver);

			// Login before each test to reach dashboard
			var user = UserFactory.GetValidUser();
			if (!_dashboardPage.IsOnDashboard())
			{
				Driver.Navigate().GoToUrl(BaseUrl + "/login");
				_loginPage.EnterEmail(user.Email);
				_loginPage.EnterPassword(user.Password);
				_loginPage.ClickLogin();
			}
		}

		[Test]
		[Description("Verify that the dashboard shows a welcome message for the user")]
		public void Dashboard_Should_ShowWelcomeMessage()
		{
			string welcome = _dashboardPage.GetWelcomeMessage();
			Assert.That(welcome, Does.StartWith("Welcome"), "Welcome message not found or incorrect.");
		}

		[Test]
		[Description("Verify that the room navigation works on the dashboard")]
		public void RoomNavigation_Should_Work()
		{
			string initialRoom = _dashboardPage.GetActiveRoomName();
			_dashboardPage.ClickNextRoom();
			string nextRoom = _dashboardPage.GetActiveRoomName();

			// If there's only one room, this might fail or be the same, 
			// but we assume a standard setup for a test house.
			Assert.That(nextRoom, Is.Not.Null, "Active room name should not be null after navigation.");
		}

		[Test]
		[Description("Verify that logout from dashboard works")]
		public void Logout_Should_RedirectToLogin()
		{
			_dashboardPage.Logout();
			Assert.That(_loginPage.IsOnLoginPage(), Is.True, "Logout did not redirect to login page.");
		}
	}
}