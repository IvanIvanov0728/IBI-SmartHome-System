using IBI_SmartHome_System.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class IdentitySeeder
	{
		public static async Task SeedAsync(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<IBI_SmartHome_System.Data.Entity.ApplicationUser>>();

			// Seed Roles
			string[] roleNames = { "Admin", "User" };
			foreach (var roleName in roleNames)
			{
				var roleExist = await roleManager.RoleExistsAsync(roleName);
				if (!roleExist)
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}

			// Seed Admin User
			var adminEmail = "admin@smarthome.com";
			var adminUser = await userManager.FindByEmailAsync(adminEmail);

			if (adminUser == null)
			{
				adminUser = new ApplicationUser
				{
					Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
					UserName = adminEmail,
					Email = adminEmail,
					EmailConfirmed = true,
					UserRole = "Admin"
				};

				var createPowerUser = await userManager.CreateAsync(adminUser, "Admin123!");
				if (createPowerUser.Succeeded)
				{
					await userManager.AddToRoleAsync(adminUser, "Admin");
				}
			}
		}
	}
}
