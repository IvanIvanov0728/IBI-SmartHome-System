using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class SecurityViewModel
	{
		public List<EntryPointViewModel> EntryPoints { get; set; } = new List<EntryPointViewModel>();
		public List<CameraViewModel> Cameras { get; set; } = new List<CameraViewModel>();
		public List<ActivityLogEntryViewModel> ActivityLog { get; set; } = new List<ActivityLogEntryViewModel>();
		public bool IsSystemArmed { get; set; }
		public string SystemStatusMessage { get; set; }
	}
}
