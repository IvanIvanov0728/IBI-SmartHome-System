using System.Text.Json.Serialization;

namespace IBI_SmartHome_System.Service.Weather
{
	public class CurrentWeather
	{
		[JsonPropertyName("temperature_2m")]
		public double Temperature { get; set; }
		[JsonPropertyName("relative_humidity_2m")]
		public int Humidity { get; set; }
		[JsonPropertyName("weather_code")]
		public int WeatherCode { get; set; }

		public string Description
		{
			get
			{
				return WeatherCode switch
				{
					0 => "Clear sky",
					1 => "Mainly clear",
					2 => "Partly Cloudy",
					3 => "Overcast",
					45 => "Fog",
					48 => "Depositing rime fog",
					51 => "Drizzle: Light",
					53 => "Drizzle: Moderate",
					55 => "Drizzle: Dense",
					56 => "Freezing Drizzle: Light",
					57 => "Freezing Drizzle: Dense",
					61 => "Rain: Slight",
					63 => "Rain: Moderate",
					65 => "Rain: Heavy ",
					66 => "Freezing Rain: Light",
					67 => "Freezing Rain: Heavy",
					71 => "Rain: Slight",
					73 => "Snow fall: Moderate",
					75 => "Snow fall: Heavy",
					77 => "Snow grains",
					80 => "Rain: Slight",
					81 => "Rain showers: Moderate",
					82 => "Rain showers: Violent",
					85 => "Snow showers: Slight",
					86 => "Snow showers: Heavy",
					95 => "Thunderstorm: Slight or moderate",
					96 => "Thunderstorm with slight hail",
					99 => "Thunderstorm with heavy hail",
					_ => "Unknown weather condition"
				};
			}
		}
	}
}
