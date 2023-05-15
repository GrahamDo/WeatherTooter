using System.Text.Json.Serialization;

namespace WeatherTooter.ApiResults;

internal class ForecastApiResults
{
    [JsonPropertyName("current_weather")] public CurrentWeatherDetails CurrentWeather { get; set; }
    public HourlyDetails Hourly { get; set; }
}