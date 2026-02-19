using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class ClimateScheduleViewModel
	{
		public int Id { get; set; }
		public string Day { get; set; }
		public string Time { get; set; }
		public string Temp { get; set; }
		public string Mode { get; set; }
	}
}
