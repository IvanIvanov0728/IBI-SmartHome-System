using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class UserProfileViewModel
	{
		public string UserName { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public string UserRole { get; set; }
	}
}
