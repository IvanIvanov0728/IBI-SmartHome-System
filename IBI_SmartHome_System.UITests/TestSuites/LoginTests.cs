using IBI_SmartHome_System.UITests.Base;
using IBI_SmartHome_System.UITests.Pages;
using NUnit.Framework;

namespace IBI_SmartHome_System.UITests.TestSuites
{
	[TestFixture]
	public class LoginTests : BaseTest
	{
		private LoginPage _loginPage;

		[SetUp]
		public void TestSetup()
		{
			_loginPage = new LoginPage(Driver);
			// Ensure we are on the login page (app might redirect if not authenticated)
			if (!_loginPage.IsOnLoginPage())
			{
				Driver.Navigate().GoToUrl(BaseUrl + "/login");
			}
		}

		[Test]
		[Description("Verify that the login page elements are displayed correctly")]
		public void LoginPage_Should_LoadCorrectly()
		{
			Assert.That(_loginPage.IsOnLoginPage(), Is.True, "The login page did not load correctly.");
		}

		[Test]
		[Description("Login attempt with empty credentials should show validation errors (browser-level)")]
		public void Login_WithEmptyCredentials_Should_NotProceed()
		{
			string initialUrl = Driver.Url;
			_loginPage.ClickLogin();

			// Depending on HTML5 validation, the URL shouldn't change
			Assert.That(Driver.Url, Is.EqualTo(initialUrl), "The login proceeded with empty credentials.");
		}

		[Test]
		[Description("Navigation to Sign Up page works")]
		public void Navigation_ToSignUp_Should_Work()
		{
			_loginPage.ClickSignUp();
			Assert.That(Driver.Url, Does.Contain("/signup"), "Navigation to Sign Up page failed.");
			Assert.That(Driver.PageSource, Does.Contain("Create Account"), "Sign Up page content not found.");
		}
	}
}
