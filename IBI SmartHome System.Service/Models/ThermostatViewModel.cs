using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class ThermostatViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Temperature { get; set; }
		public int TargetTemperature { get; set; }
		public float Humidity { get; set; }
	}
}
