using IBI_SmartHome_System.Service.Weather;
using System.Collections.Generic;

namespace IBI_SmartHome_System.Service.Models
{
    public class DashboardViewModel
    {
		public List<LightControlViewModel> Lights { get; set; }
		public List<RoomViewModel> Rooms { get; set; }
		public int TargetTemperature { get; set; }
		public List<SceneViewModel> Scenes { get; set; }

		public double CurrentTemperature { get; set; }

		public WeatherApiResponse WeatherOutside { get; set; }
	}
}
