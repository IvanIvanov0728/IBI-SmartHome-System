using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class EntryPointViewModel
	{
		public int DeviceId { get; set; }
		public string Name { get; set; }
		public bool IsLocked { get; set; }
		public string Type { get; set; } // e.g., "Door", "Window"
		public string RoomName { get; set; }
	}
}
