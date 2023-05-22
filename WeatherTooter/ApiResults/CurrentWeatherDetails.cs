using System.Runtime.CompilerServices;
using System.Text;

namespace WeatherTooter.ApiResults;

public class CurrentWeatherDetails
{
    public float Temperature { get; set; }
    public byte WeatherCode { get; set; }

    public string GetCurrentConditions()
    {
        // From https://open-meteo.com/en/docs (Search page for "WMO")
        var result = new StringBuilder();
        if (WeatherCode < 51)
        {
            switch (WeatherCode)
            {
                case 0: return "clear";
                case 1: return "mostly clear";
                case 2: return "partly cloudy";
                case 3: return "overcast";
                case 45:
                case 48:
                    result.Append("misty");
                    if (WeatherCode == 48)
                        result.Append(" with rime fog");
                    return result.ToString();
            }
        }

        // Type of precipitation
        if (WeatherCode is > 50 and < 60)
            result.Append("drizzling");
        else if (WeatherCode is > 60 and < 70 || WeatherCode is >= 80 and <= 82 || WeatherCode >= 95)
            result.Append("raining");
        else if (WeatherCode is > 70 and < 80 || WeatherCode == 85 || WeatherCode == 86)
            result.Append("snowing");

        // Intensity of precipitation
        if (WeatherCode is 51 or 56 or 61 or 66 or 71)
            result.Append(" slightly");
        else if (WeatherCode is 80 or 85)
            result.Append(" with light");
        else if (WeatherCode is 81 or 95)
            result.Append(" with");
        else if (WeatherCode is 82 or 86)
            result.Append(" with heavy");
        else if (WeatherCode is 55 or 57 or 67)
            result.Append(" heavily");
        
        if (WeatherCode is 56 or 57 or 66 or 67)
            result.Append(" (freezing)");

        // Showers / Thunderstorms
        if (WeatherCode is 80 or 81 or 82 or 86 or 85)
            result.Append(" showers");
        else if (WeatherCode is 95 or 96 or 99)
        {
            result.Append(" thunderstorms");
            if (WeatherCode is 96)
                result.Append(" and hail");
            else if (WeatherCode is 99)
                result.Append(" and heavy hail");
        }

        return result.Length > 0 ? 
            result.ToString() : 
            $"*** Unknown WeatherCode: {WeatherCode} ***";
    }
}