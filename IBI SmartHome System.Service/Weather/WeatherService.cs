using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Weather
{
	public class WeatherService
	{
		private readonly HttpClient _httpClient;
		public WeatherService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<WeatherApiResponse> GetWeatherAsync(double latitude = 42.70, double longitude = 23.32)
		{
			var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,weather_code";
			return await _httpClient.GetFromJsonAsync<WeatherApiResponse>(url);
		}
	}
}
