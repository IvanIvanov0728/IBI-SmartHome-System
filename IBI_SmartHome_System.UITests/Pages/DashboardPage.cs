using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace IBI_SmartHome_System.UITests.Pages
{
	public class DashboardPage
	{
		private readonly IWebDriver _driver;
		private readonly WebDriverWait _wait;

		public DashboardPage(IWebDriver driver)
		{
			_driver = driver;
			_wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		}

		// Locators
		private By WelcomeMessage => By.CssSelector("h1.text-4xl");
		private By WeatherWidget => By.CssSelector(".flex.items-center.gap-2.px-4.py-2.bg-white");
		private By ActiveRoomName => By.CssSelector("h2.text-3xl.font-display");
		private By NextRoomButton => By.CssSelector("button .lucide-chevron-right");
		private By PrevRoomButton => By.CssSelector("button .lucide-chevron-left");
		private By LogoutButton => By.CssSelector("button .lucide-log-out");

		// Actions
		public string GetWelcomeMessage()
		{
			var element = _wait.Until(ExpectedConditions.ElementIsVisible(WelcomeMessage));
			return element.Text;
		}

		public string GetActiveRoomName()
		{
			var element = _wait.Until(ExpectedConditions.ElementIsVisible(ActiveRoomName));
			return element.Text;
		}

		public void ClickNextRoom()
		{
			// Hover might be needed if button is only visible on group-hover
			var element = _wait.Until(ExpectedConditions.ElementToBeClickable(NextRoomButton));
			element.Click();
		}

		public void Logout()
		{
			var element = _wait.Until(ExpectedConditions.ElementToBeClickable(LogoutButton));
			element.Click();
		}

		public bool IsOnDashboard()
		{
			return _driver.Url.EndsWith("/") || _driver.Url.Contains("/dashboard");
		}
	}
}