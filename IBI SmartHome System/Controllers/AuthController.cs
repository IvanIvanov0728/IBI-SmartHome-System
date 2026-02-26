using IBI_SmartHome_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IBI_SmartHome_System.Data.Entity;

namespace IBI_SmartHome_System.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IBI_SmartHome_System.Data.Entity.ApplicationUser> _userManager;
		private readonly SignInManager<IBI_SmartHome_System.Data.Entity.ApplicationUser> _signInManager;

		public AuthController(UserManager<IBI_SmartHome_System.Data.Entity.ApplicationUser> userManager, SignInManager<IBI_SmartHome_System.Data.Entity.ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new IBI_SmartHome_System.Data.Entity.ApplicationUser { UserName = model.Email, Email = model.Email, UserRole = "User" };
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
					return Ok(new { message = "Registration successful" });
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return BadRequest(ModelState);
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				// This doesn't count login failures towards account lockout
				// To enable password failures to trigger account lockout, set lockoutOnFailure: true
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					return Ok(new { message = "Login successful" });
				}
				if (result.RequiresTwoFactor)
				{
					return BadRequest(new { message = "Requires two factor authentication" });
				}
				if (result.IsLockedOut)
				{
					return BadRequest(new { message = "User account locked out" });
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return BadRequest(new { message = "Invalid login attempt" });
				}
			}

			return BadRequest(ModelState);
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok(new { message = "Logged out" });
		}

		[HttpGet("status")]
		public IActionResult Status()
		{
			var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

			if (User.Identity?.IsAuthenticated == true)
			{
				return Ok(new
				{
					isAuthenticated = true,
					username = User.Identity.Name,
					claimCount = claims.Count,
					claims = claims
				});
			}
			return Ok(new { isAuthenticated = false, claimCount = 0 });
		}
	}
}
