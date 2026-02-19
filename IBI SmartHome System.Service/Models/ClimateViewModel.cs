using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class ClimateViewModel
	{
		public double CurrentTemperature { get; set; }
		public int TargetTemperature { get; set; }
		public List<ThermostatViewModel> Thermostats { get; set; }
	}
}
