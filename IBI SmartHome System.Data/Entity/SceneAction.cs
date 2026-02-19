using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace IBI_SmartHome_System.Data.Entity
{
	public class SceneAction
	{
		public int Id { get; set; }

		[Required]
		public int SceneId { get; set; }

		[ForeignKey("SceneId")]
		public Scene Scene { get; set; }

		[Required]
		public int DeviceId { get; set; }

		[ForeignKey("DeviceId")]
		public Device Device { get; set; }

		[Required]
		public string Property { get; set; } // e.g., "Power", "Brightness", "Color", "TemperatureValue"

		[Required]
		public string Value { get; set; } // e.g., "false", "80", "Red", "22.5"
	}
}
