using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class ClimateSchedule
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Day { get; set; }

		public int HouseId { get; set; } // Link to House
		public House House { get; set; } // Navigation property

		[Required]
		public string Time { get; set; }

		[Required]
		public string Temp { get; set; }

		[Required]
		public string Mode { get; set; }
	}
}
