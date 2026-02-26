using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class ActivityLogEntry
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int HouseId { get; set; } // Link to House
		public House House { get; set; } // Navigation property

		[Required]
		public DateTime Timestamp { get; set; }

		[Required]
		public string Event { get; set; } // e.g., "Front Door Locked", "Motion Detected"

		public string? Type { get; set; } // e.g., "info", "warning", "success"

		public int? DeviceId { get; set; }
		[ForeignKey("DeviceId")]
		public Device? Device { get; set; }
	}
}
