using Newtonsoft.Json;

namespace WeatherTooter.ApiResults;

public class ForecastApiResults
{
    [JsonProperty("current_weather")] public CurrentWeatherDetails CurrentWeather { get; set; }
    public HourlyDetails Hourly { get; set; }

    public ForecastApiResults()
    {
        CurrentWeather = new CurrentWeatherDetails();
        Hourly = new HourlyDetails();
    }
}