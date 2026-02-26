using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Data.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class DeviceSeeder
	{
		public static IEnumerable<Device> Seed()
		{
			int adminHouseId = 1;

			List<Device> devices = new List<Device>()
			{
				new Device()
				{
					Id = 101,
					RoomId = 11,
					Name = "Living Room Lamp",
					Type = DeviceType.Lamp,
					MqttTopic = "telemetry/lamp/livingroom",
					HouseId = adminHouseId // The house it belongs to
				},
				new Device()
				{
					Id = 401,
					RoomId = 11,
					Name = "Living Room Motion Sensor",
					Type = DeviceType.MotionSensor,
					MqttTopic = "telemetry/motionsensor/livingroom",
					HouseId = adminHouseId
				},
				new Device()
				{
					Id = 701,
					RoomId = 11,
					Name = "Living Room Temperature Sensor",
					Type = DeviceType.TemperatureSensor,
					MqttTopic = "telemetry/tempsensor/livingroom",
					HouseId = adminHouseId
				},
                // Security Devices for SecurityPage.tsx
                new Device()
				{
					Id = 801,
					RoomId = 11, // Living Room
                    Name = "Front Door",
					Type = DeviceType.Generic, // Use generic for security devices
                    MqttTopic = "security/door/front",
					HouseId = adminHouseId,
					IsDoor = true,
					IsLocked = true
				},
				new Device()
				{
					Id = 802,
					RoomId = 11, // Living Room
                    Name = "Back Door",
					Type = DeviceType.Generic,
					MqttTopic = "security/door/back",
					HouseId = adminHouseId,
					IsDoor = true,
					IsLocked = false
				},
				new Device()
				{
					Id = 803,
					RoomId = 12, // Guest Bedroom
                    Name = "Guest Window",
					Type = DeviceType.Generic,
					MqttTopic = "security/window/guest",
					HouseId = adminHouseId,
					IsWindow = true,
					IsLocked = true
				}
			};
			return devices;
		}
	}
}
