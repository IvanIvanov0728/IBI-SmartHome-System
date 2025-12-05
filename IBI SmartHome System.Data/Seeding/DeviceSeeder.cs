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
			// for now i will only add one of each device type in the Livingroom/Kitchen

			List<Device> devices = new List<Device>()
			{
				new Device()
				{
					Id = 101,
					RoomId = 11,
					Name = "Living Room Lamp",
					Type = DeviceType.Lamp,
					MqttTopic = "telemetry/lamp/livingroom"
				},
				new Device()
				{
					Id = 401,
					RoomId = 11,
					Name = "Living Room Motion Sensor",
					Type = DeviceType.MotionSensor,
					MqttTopic = "telemetry/motionsensor/livingroom"
				},
				new Device()
				{
					Id = 701,
					RoomId = 11,
					Name = "Living Room Temperature Sensor",
					Type = DeviceType.TemperatureSensor,
					MqttTopic = "telemetry/tempsensor/livingroom"
				}
			};
			return devices;
		}
	}
}
