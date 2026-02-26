using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class CameraViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string StreamUrl { get; set; }
		public bool IsLive { get; set; }
	}
}
