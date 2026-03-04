using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class HouseHierarchyViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UserEmail { get; set; }
		public List<RoomHierarchyViewModel> Rooms { get; set; } = new();
	}
}
