using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class ApplicationUser : IdentityUser
	{
		// Custom properties for user profile and settings
		public bool ReceiveNotifications { get; set; } = true;
		public bool DarkModeEnabled { get; set; } = false;

		// Example: For the "Admin" role status display
		public string UserRole { get; set; } = "User"; // Will be updated during seeding
	}
}
