using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class CreateAutomationRuleViewModel
	{
		[Required]
		public string Name { get; set; }
		public int TriggerDeviceId { get; set; }
		public string TriggerType { get; set; }
		public double? ConditionValue { get; set; }
		public int ActionDeviceId { get; set; }
		public string ActionType { get; set; }
		public string ActionValue { get; set; }
	}
}
