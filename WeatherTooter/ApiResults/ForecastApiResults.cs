using Newtonsoft.Json;

namespace WeatherTooter.ApiResults;

public class ForecastApiResults
{
    [JsonProperty("current_weather")] public CurrentWeatherDetails CurrentWeather { get; set; } = new();
    public HourlyDetails Hourly { get; set; } = new();
}