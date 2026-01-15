using IBI_SmartHome_System.Service.Weather;
using System.Collections.Generic;

namespace IBI_SmartHome_System.Service.Models
{
    public class DashboardViewModel
    {
		public List<LightControlViewModel> Lights { get; set; }

		public int TargetTemperature { get; set; }

		public  double CurrentTemperature { get; set; }

		public WeatherApiResponse WeatherOutside { get; set; }
	}
}
