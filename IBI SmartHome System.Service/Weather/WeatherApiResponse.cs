using System.Text.Json.Serialization;

namespace IBI_SmartHome_System.Service.Weather
{
	public class WeatherApiResponse
	{
		[JsonPropertyName("current")]
		public CurrentWeather Current { get; set; }

	}
}
