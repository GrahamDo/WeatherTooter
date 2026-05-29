using Newtonsoft.Json;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// All properties need public getters and setters for serialisation to work

namespace WeatherTooter.ApiResults;

public class ForecastApiResults
{
    [JsonProperty("current_weather")] public CurrentWeatherDetails CurrentWeather { get; set; } = new();
    public HourlyDetails Hourly { get; set; } = new();
}