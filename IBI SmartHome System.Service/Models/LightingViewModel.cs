using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class LightingViewModel
	{
		public List<LightControlViewModel> Lights { get; set; }
		public List<RoomViewModel> Rooms { get; set; }
	}
}
