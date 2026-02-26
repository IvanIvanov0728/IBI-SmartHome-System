using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class House
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string? Address { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }

		[Required]
		public string UserId { get; set; } // Owner of the house
		public ApplicationUser User { get; set; } // Navigation property to the owner

		// Navigation properties for entities belonging to this House
		public ICollection<Room> Rooms { get; set; } = new List<Room>();
		public ICollection<Scene> Scenes { get; set; } = new List<Scene>();
		public ICollection<ClimateSchedule> ClimateSchedules { get; set; } = new List<ClimateSchedule>();
		public ICollection<ActivityLogEntry> ActivityLogEntries { get; set; } = new List<ActivityLogEntry>();
		public ICollection<Camera> Cameras { get; set; } = new List<Camera>();
	}
}
