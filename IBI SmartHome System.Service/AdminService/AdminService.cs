using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Data.Entity.Enum;
using IBI_SmartHome_System.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.AdminService
{
	public class AdminService : IAdminService
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public AdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<List<UserSearchResultViewModel>> SearchUsersAsync(string query)
		{
			if (string.IsNullOrWhiteSpace(query)) return new List<UserSearchResultViewModel>();

			return await _userManager.Users
				.Where(u => u.Email.Contains(query) || u.UserName.Contains(query))
				.Select(u => new UserSearchResultViewModel
				{
					Id = u.Id,
					Username = u.UserName,
					Email = u.Email
				})
				.Take(10)
				.ToListAsync();
		}

		public async Task<int> CreateHouseAsync(CreateHouseViewModel model)
		{
			var house = new House
			{
				Name = model.Name,
				Address = model.Address,
				UserId = model.UserId
			};

			_context.Houses.Add(house);
			await _context.SaveChangesAsync();

			await LogActionAsync(house.Id, $"House '{house.Name}' created and assigned to user.", "success");

			return house.Id;
		}

		public async Task<int> AddRoomToHouseAsync(CreateRoomViewModel model)
		{
			var room = new Room
			{
				Name = model.Name,
				Floor = model.Floor,
				HouseId = model.HouseId
			};

			_context.Room.Add(room);
			await _context.SaveChangesAsync();

			await LogActionAsync(model.HouseId, $"Room '{room.Name}' added to floor {room.Floor}.", "info");

			return room.Id;
		}

		public async Task<int> AddDeviceToRoomAsync(CreateDeviceViewModel model)
		{
			var room = await _context.Room.FindAsync(model.RoomId);
			if (room == null) throw new ArgumentException("Room not found");

			DeviceType type = Enum.TryParse<DeviceType>(model.Type, true, out var result)
				? result
				: DeviceType.Generic;

			var device = new Device
			{
				Name = model.Name,
				Type = type,
				RoomId = model.RoomId,
				HouseId = room.HouseId,
				MqttTopic = model.MqttTopic
			};

			_context.Device.Add(device);
			await _context.SaveChangesAsync();

			// Initialize specific device entities based on type
			if (type == DeviceType.Lamp)
			{
				_context.Lamps.Add(new Lamp { DeviceId = device.Id, IsOn = false, Brightness = 0 });
			}
			else if (type == DeviceType.TemperatureSensor)
			{
				_context.Temperature.Add(new Temperature { DeviceId = device.Id, TemperatureValue = 20 });
			}
			else if (type == DeviceType.MotionSensor)
			{
				_context.MotionSensor.Add(new MotionSensor { DeviceId = device.Id, IsMotionDetected = false });
			}

			await _context.SaveChangesAsync();
			await LogActionAsync(room.HouseId, $"Device '{device.Name}' ({type}) added to room '{room.Name}'.", "success", device.Id);

			return device.Id;
		}

		public async Task<List<HouseHierarchyViewModel>> GetHousesWithHierarchyAsync()
		{
			return await _context.Houses
				.Include(h => h.User)
				.Include(h => h.Rooms)
					.ThenInclude(r => r.Devices)
				.Select(h => new HouseHierarchyViewModel
				{
					Id = h.Id,
					Name = h.Name,
					UserEmail = h.User.Email,
					Rooms = h.Rooms.Select(r => new RoomHierarchyViewModel
					{
						Id = r.Id,
						Name = r.Name,
						Devices = r.Devices.Select(d => new DeviceHierarchyViewModel
						{
							Id = d.Id,
							Name = d.Name,
							Type = d.Type.ToString()
						}).ToList()
					}).ToList()
				})
				.ToListAsync();
		}

		public async Task<List<AdminActivityLogViewModel>> GetGlobalActivityLogsAsync()
		{
			return await _context.ActivityLogEntries
				.Include(a => a.House)
				.Include(a => a.Device)
				.OrderByDescending(a => a.Timestamp)
				.Take(100)
				.Select(a => new AdminActivityLogViewModel
				{
					Id = a.Id,
					HouseId = a.HouseId,
					HouseName = a.House.Name,
					UserEmail = a.House.User.Email,
					Timestamp = a.Timestamp,
					Event = a.Event,
					Type = a.Type,
					DeviceName = a.Device != null ? a.Device.Name : "N/A"
				})
				.ToListAsync();
		}

		public async Task LogActionAsync(int houseId, string eventDescription, string type, int? deviceId = null)
		{
			var log = new ActivityLogEntry
			{
				HouseId = houseId,
				Event = eventDescription,
				Type = type,
				Timestamp = DateTime.UtcNow,
				DeviceId = deviceId
			};

			_context.ActivityLogEntries.Add(log);
			await _context.SaveChangesAsync();
		}

		public async Task<AdminAnalyticsViewModel> GetGlobalAnalyticsAsync()
		{
			var random = new Random();
			var weekly = new List<WeeklyDataPoint>();
			var days = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

			foreach (var day in days)
			{
				weekly.Add(new WeeklyDataPoint { Name = day, Usage = random.Next(100, 500), Solar = random.Next(50, 200) });
			}

			var hourly = new List<HourlyDataPoint>();
			for (int i = 0; i < 24; i++)
			{
				hourly.Add(new HourlyDataPoint { Time = $"{i:00}:00", Value = random.Next(5, 50) });
			}

			var distribution = new List<RoomUsage>();
			var roomNames = new[] { "Living Room", "Kitchen", "Bedroom", "Office", "Garage" };
			var colors = new[] { "bg-blue-400", "bg-emerald-400", "bg-purple-400", "bg-amber-400", "bg-rose-400" };

			for (int i = 0; i < roomNames.Length; i++)
			{
				distribution.Add(new RoomUsage
				{
					Room = roomNames[i],
					Usage = $"{random.Next(20, 100)} kWh",
					Color = colors[i],
					Percent = random.Next(10, 30)
				});
			}

			return new AdminAnalyticsViewModel
			{
				SystemEnergyWeekly = weekly,
				SystemActivityHourly = hourly,
				RoomDistribution = distribution
			};
		}

		public async Task<List<AutomationRuleViewModel>> GetAutomationRulesAsync()
		{
			return await _context.AutomationRules
				.Include(r => r.TriggerDevice)
				.Include(r => r.ActionDevice)
				.Select(r => new AutomationRuleViewModel
				{
					Id = r.Id,
					Name = r.Name,
					TriggerDeviceName = r.TriggerDevice.Name,
					TriggerType = r.TriggerType,
					ActionDeviceName = r.ActionDevice.Name,
					ActionType = r.ActionType,
					IsActive = r.IsActive
				})
				.ToListAsync();
		}

		public async Task<int> CreateAutomationRuleAsync(CreateAutomationRuleViewModel model)
		{
			var rule = new AutomationRule
			{
				Name = model.Name,
				TriggerDeviceId = model.TriggerDeviceId,
				TriggerType = model.TriggerType,
				ConditionValue = model.ConditionValue,
				ActionDeviceId = model.ActionDeviceId,
				ActionType = model.ActionType,
				ActionValue = model.ActionValue,
				IsActive = true
			};

			_context.AutomationRules.Add(rule);
			await _context.SaveChangesAsync();
			return rule.Id;
		}

		public async Task DeleteAutomationRuleAsync(int id)
		{
			var rule = await _context.AutomationRules.FindAsync(id);
			if (rule != null)
			{
				_context.AutomationRules.Remove(rule);
				await _context.SaveChangesAsync();
			}
		}
	}
}
