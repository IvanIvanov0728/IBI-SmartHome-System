using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class Camera
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int HouseId { get; set; } // Link to House
		public House House { get; set; } // Navigation property

		[Required]
		public string Name { get; set; } // e.g., "Front Porch Camera"

		[Required]
		public string StreamUrl { get; set; } // URL to the camera feed (mock for now)

		public bool IsLive { get; set; } = false; // Indicates if it's currently streaming live
	}
}
