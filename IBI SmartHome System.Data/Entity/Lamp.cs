using IBI_SmartHome_System.Data.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class Lamp
	{
		public int Id { get; set; }
		public int DeviceId { get; set; }
		public Device? Device { get; set; }

		public bool IsOn { get; set; }
		public int Brightness { get; set; } // brightness (lumens) from 450 to 5800
											// look in to LampColors enum for color options
		public LampColors Color { get; set; } // color temperature (kelvins or warm/cool white)  from 2600K to 8000K sutrin i prizdrachavane po niski kelvini v sredata na denq po visoki 

		// public RGB Color { get; set; } // RGB color representation (if applicable)

		// public int Dimmer { get; set; } // dimmer level from 0 to 100%
	}
}
