using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class AutomationRule
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		// Trigger Details
		public int TriggerDeviceId { get; set; }
		[ForeignKey("TriggerDeviceId")]
		public Device TriggerDevice { get; set; }

		public string TriggerType { get; set; } // e.g., "MotionDetected", "TempAbove", "TempBelow"
		public double? ConditionValue { get; set; }

		// Action Details
		public int ActionDeviceId { get; set; }
		[ForeignKey("ActionDeviceId")]
		public Device ActionDevice { get; set; }

		public string ActionType { get; set; } // e.g., "TurnOn", "TurnOff", "SetTemperature"
		public string ActionValue { get; set; }

		public bool IsActive { get; set; } = true;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
