using WeatherTooter.ApiResults;

namespace WeatherTooter;

public class MinMaxForecast
{
    public string MinMaxDescriptor { get; set; }
    public float MinMaxValue { get; set; }
    public float MaxPrecipitationChance { get; set; }
    public string PrecipitationChanceArticle { get; set; }

    public MinMaxForecast()
    {
        MinMaxDescriptor = string.Empty;
        MaxPrecipitationChance = 0;
    }

    public static MinMaxForecast GetFromApiResults(ForecastApiResults weather, byte hoursToForecast)
    {
        var minimumTemperature = float.MinValue;
        var maximumTemperature = float.MaxValue;

        var startIndex = weather.CurrentWeather.LocalTime.Hour + 1;
        var endIndex = startIndex + hoursToForecast;

        var result = new MinMaxForecast();
        for (var i = startIndex; i < endIndex; i++)
        {
            var temperature = weather.Hourly.Temperatures[i];
            if (temperature > maximumTemperature || maximumTemperature == float.MaxValue)
                maximumTemperature = temperature;
            if (temperature < minimumTemperature || minimumTemperature == float.MinValue)
                minimumTemperature = temperature;

            var precipitation = weather.Hourly.PrecipitationProbability[i];
            if (precipitation > result.MaxPrecipitationChance)
                result.MaxPrecipitationChance = precipitation;
        }

        if (maximumTemperature <= weather.CurrentWeather.Temperature)
        {
            result.MinMaxDescriptor = "minimum";
            result.MinMaxValue = minimumTemperature;
        }
        else
        {
            result.MinMaxDescriptor = "maximum";
            result.MinMaxValue = maximumTemperature;
        }

        return result;
    }
}