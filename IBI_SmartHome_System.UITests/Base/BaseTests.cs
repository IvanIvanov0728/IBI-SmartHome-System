using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace IBI_SmartHome_System.UITests.Base
{
	public abstract class BaseTest
	{
		protected IWebDriver Driver;
		protected string BaseUrl = "http://localhost:5173"; // Default Vite dev port

		[SetUp]
		public void Setup()
		{
			var options = new ChromeOptions();
			// options.AddArgument("--headless"); // Uncomment for CI
			options.AddArgument("--start-maximized");
			options.AddArgument("--disable-notifications");

			// In a real environment we might need to point to the chromedriver path
			Driver = new ChromeDriver(options);
			Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			Driver.Navigate().GoToUrl(BaseUrl);
		}

		[TearDown]
		public void Teardown()
		{
			if (Driver != null)
			{
				Driver.Quit();
				Driver.Dispose();
			}
		}
	}
}