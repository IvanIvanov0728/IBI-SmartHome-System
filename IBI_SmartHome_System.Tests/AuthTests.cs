using IBI_SmartHome_System.Controllers;
using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using NUnit.Framework;

namespace IBI_SmartHome_System.Tests
{
	[TestFixture]
	public class AuthTests
	{
		private Mock<UserManager<ApplicationUser>> _userManagerMock;
		private Mock<SignInManager<ApplicationUser>> _signInManagerMock;
		private AuthController _controller;

		[SetUp]
		public void Setup()
		{
			var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
			_userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

			var contextAccessorMock = new Mock<IHttpContextAccessor>();
			var userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
			_signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
				_userManagerMock.Object,
				contextAccessorMock.Object,
				userClaimsPrincipalFactoryMock.Object,
				null, null, null, null);

			_controller = new AuthController(_userManagerMock.Object, _signInManagerMock.Object);
		}

		[Test]
		public async Task Register_ReturnsOk_WhenSucceeded()
		{
			var model = new RegisterViewModel { Email = "test@test.com", Password = "Password123!" };
			_userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
				.ReturnsAsync(IdentityResult.Success);

			var result = await _controller.Register(model);

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task Login_ReturnsOk_WhenSucceeded()
		{
			var model = new LoginViewModel { Email = "test@test.com", Password = "Password123!" };
			_signInManagerMock.Setup(x => x.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false))
				.ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

			var result = await _controller.Login(model);

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}

		[Test]
		public async Task Register_ReturnsBadRequest_WhenFailed()
		{
			var model = new RegisterViewModel { Email = "test@test.com", Password = "123" };
			_userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
				.ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password too short" }));

			var result = await _controller.Register(model);

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}

		[Test]
		public async Task Login_ReturnsBadRequest_WhenFailed()
		{
			var model = new LoginViewModel { Email = "wrong@test.com", Password = "WrongPassword" };
			_signInManagerMock.Setup(x => x.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false))
				.ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

			var result = await _controller.Login(model);

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
		}

		[Test]
		public async Task Login_ReturnsBadRequest_WhenLockedOut()
		{
			var model = new LoginViewModel { Email = "locked@test.com", Password = "Password123!" };
			_signInManagerMock.Setup(x => x.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false))
				.ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.LockedOut);

			var result = await _controller.Login(model);

			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
			var badRequestResult = (BadRequestObjectResult)result;
			Assert.That(badRequestResult.Value.ToString(), Does.Contain("User account locked out"));
		}

		[Test]
		public async Task Logout_ReturnsOk()
		{
			var result = await _controller.Logout();
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
			_signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
		}

		[Test]
		public void Status_ReturnsCorrectInfo()
		{
			var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
				new Claim(ClaimTypes.Name, "test@test.com")
			}, "mock"));

			_controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { User = user }
			};

			var result = _controller.Status();
			Assert.That(result, Is.InstanceOf<OkObjectResult>());
		}
	}
}