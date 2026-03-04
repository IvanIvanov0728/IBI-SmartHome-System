using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class CreateRoomViewModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Floor { get; set; }

		[Required]
		public int HouseId { get; set; }
	}
}
