using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class EnergyViewModel
	{
		public IEnumerable<WeeklyDataPoint> WeeklyData { get; set; }
		public IEnumerable<HourlyDataPoint> HourlyData { get; set; }
		public IEnumerable<RoomUsage> RoomData { get; set; }
		public EnvironmentalImpact EnvironmentalImpact { get; set; }
		public BatteryStorage BatteryStorage { get; set; }
	}
}
