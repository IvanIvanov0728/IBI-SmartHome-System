using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class WeeklyDataPoint
	{
		public string Name { get; set; }
		public int Usage { get; set; }
		public int Solar { get; set; }
	}
}
