using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class Room
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Floor { get; set; }

		public int HouseId { get; set; } // Link to House
		public House House { get; set; } // Navigation property

		public ICollection<Device> Devices { get; set; } = new List<Device>();
	}
}
