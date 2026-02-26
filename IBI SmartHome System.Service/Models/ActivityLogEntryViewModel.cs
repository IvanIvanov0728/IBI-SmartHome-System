using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class ActivityLogEntryViewModel
	{
		public int Id { get; set; }
		public DateTime Timestamp { get; set; }
		public string Event { get; set; }
		public string Type { get; set; } // e.g., "info", "warning", "success"
	}
}
