using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class CreateHouseViewModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string UserId { get; set; }
	}
}
