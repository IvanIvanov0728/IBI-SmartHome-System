using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace IBI_SmartHome_System.UITests.Pages
{
	public class LoginPage
	{
		private readonly IWebDriver _driver;
		private readonly WebDriverWait _wait;

		public LoginPage(IWebDriver driver)
		{
			_driver = driver;
			_wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		}

		// Locators
		private By EmailInput => By.CssSelector("input[type='email']");
		private By PasswordInput => By.CssSelector("input[type='password']");
		private By LoginButton => By.CssSelector("button[type='submit']");
		private By SignUpLink => By.XPath("//a[contains(text(), 'Sign up')]");
		private By RememberMeCheckbox => By.Id("remember");

		// Actions
		public void EnterEmail(string email)
		{
			var element = _wait.Until(ExpectedConditions.ElementIsVisible(EmailInput));
			element.Clear();
			element.SendKeys(email);
		}

		public void EnterPassword(string password)
		{
			var element = _wait.Until(ExpectedConditions.ElementIsVisible(PasswordInput));
			element.Clear();
			element.SendKeys(password);
		}

		public void ClickLogin()
		{
			var element = _wait.Until(ExpectedConditions.ElementToBeClickable(LoginButton));
			element.Click();
		}

		public void ClickSignUp()
		{
			var element = _wait.Until(ExpectedConditions.ElementToBeClickable(SignUpLink));
			element.Click();
		}

		public void ToggleRememberMe()
		{
			var element = _wait.Until(ExpectedConditions.ElementToBeClickable(RememberMeCheckbox));
			element.Click();
		}

		public bool IsOnLoginPage()
		{
			return _driver.Url.Contains("/login") || _driver.PageSource.Contains("Welcome Back");
		}
	}
}