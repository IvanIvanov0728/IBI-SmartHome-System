using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class AdminAnalyticsViewModel
	{
		public List<WeeklyDataPoint> SystemEnergyWeekly { get; set; } = new();
		public List<HourlyDataPoint> SystemActivityHourly { get; set; } = new();
		public List<RoomUsage> RoomDistribution { get; set; } = new();
	}
}
