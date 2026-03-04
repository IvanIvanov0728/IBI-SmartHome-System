using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class AutomationRuleViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string TriggerDeviceName { get; set; }
		public string TriggerType { get; set; }
		public string ActionDeviceName { get; set; }
		public string ActionType { get; set; }
		public bool IsActive { get; set; }
	}
}
