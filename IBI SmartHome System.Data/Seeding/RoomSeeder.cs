using IBI_SmartHome_System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class RoomSeeder
	{
		public static IEnumerable<Room> Seed()
		{
			List<Room> rooms = new List<Room>
			{
				new Room
				{
					Id = 11,
					Floor = "First",
					Name = "Living Room/Kitchen"
				},
				new Room
				{
					Id = 12,
					Floor = "First",
					Name = "Guest Bedroom"
				},
				new Room
				{
					Id = 13,
					Floor = "First",
					Name = "Utility"
				},
				new Room
				{
					Id = 14,
					Floor = "First",
					Name = "Bathroom"
				},
				new Room
				{
					Id = 15,
					Floor = "First",
					Name = "Hallway"
				},
				new Room
				{
					Id = 16,
					Floor = "First",
					Name = "Mudroom"
				},
				new Room
				{
					Id = 21,
					Floor = "Second",
					Name = "Master Bedroom"
				},
				new Room
				{
					Id = 22,
					Floor = "Second",
					Name = "Master Bathroom"
				},
				 new Room
				{
					Id = 23,
					Floor = "Second",
					Name = "Ivan Bedroom"
				},
				new Room
				{
					Id = 24,
					Floor = "Second",
					Name = "Ivan Bathroom"
				},
				 new Room
				{
					Id = 25,
					Floor = "Second",
					Name = "Neli Bedroom"
				},
				new Room
				{
					Id = 26,
					Floor = "Second",
					Name = "Neli Bathroom"
				},
				new Room
				{
					Id = 27,
					Floor = "Second",
					Name = "Hallway"
				},
				new Room
				{
					Id = 28,
					Floor = "Second",
					Name = "Office"
				},
				new Room
				{
					Id = 99,
					Floor = "Ground",
					Name = "Outdoor"
				},
			};
			return rooms;
		}

	}
}
