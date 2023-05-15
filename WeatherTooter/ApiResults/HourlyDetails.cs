using System.Text.Json.Serialization;

namespace WeatherTooter.ApiResults;

internal class HourlyDetails
{
    [JsonPropertyName("temperature_2m")] public float[] Temperatures { get; set; }
    [JsonPropertyName("apparent_temperature")] public float[] ApparentTemperatures { get; set; }
    [JsonPropertyName("precipitation_probability")] public float[] PrecipitatioProbability { get; set; }
}