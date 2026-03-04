using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class AdminActivityLogViewModel
	{
		public int Id { get; set; }
		public int HouseId { get; set; }
		public string HouseName { get; set; }
		public string UserEmail { get; set; }
		public DateTime Timestamp { get; set; }
		public string Event { get; set; }
		public string Type { get; set; }
		public string DeviceName { get; set; }
	}
}
