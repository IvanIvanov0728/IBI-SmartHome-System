using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class RoomUsage
	{
		public string Room { get; set; }
		public string Usage { get; set; }
		public string Color { get; set; }
		public int Percent { get; set; }
	}
}
