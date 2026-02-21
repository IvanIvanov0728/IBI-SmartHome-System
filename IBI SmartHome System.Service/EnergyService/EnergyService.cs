using IBI_SmartHome_System.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.EnergyService
{
	public class EnergyService : IEnergyService
	{
		public Task<EnergyViewModel> GetEnergyDataAsync()
		{
			var random = new Random();
			var weeklyData = new List<WeeklyDataPoint>();
			var hourlyData = new List<HourlyDataPoint>();
			var roomData = new List<RoomUsage>();

			for (int i = 0; i < 7; i++)
			{
				weeklyData.Add(new WeeklyDataPoint { Name = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" }[i], Usage = random.Next(30, 60), Solar = random.Next(10, 25) });
			}

			for (int i = 0; i < 7; i++)
			{
				hourlyData.Add(new HourlyDataPoint { Time = $"{i * 4:00}:00", Value = random.NextDouble() * 5 });
			}

			roomData.Add(new RoomUsage { Room = "Living Room", Usage = "12.4 kWh", Color = "bg-blue-400", Percent = 35 });
			roomData.Add(new RoomUsage { Room = "Kitchen", Usage = "8.2 kWh", Color = "bg-orange-400", Percent = 25 });
			roomData.Add(new RoomUsage { Room = "Master Bedroom", Usage = "6.1 kWh", Color = "bg-purple-400", Percent = 18 });
			roomData.Add(new RoomUsage { Room = "Other", Usage = "5.8 kWh", Color = "bg-gray-400", Percent = 22 });


			var viewModel = new EnergyViewModel
			{
				WeeklyData = weeklyData,
				HourlyData = hourlyData,
				RoomData = roomData,
				EnvironmentalImpact = new EnvironmentalImpact { Co2Offset = 42.8, TreesSaved = 3 },
				BatteryStorage = new BatteryStorage { Percentage = 88, EstimatedTimeRemaining = "14h 20m" }
			};

			return Task.FromResult(viewModel);
		}
	}
}
