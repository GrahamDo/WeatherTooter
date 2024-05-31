using Newtonsoft.Json;

namespace WeatherTooter.ApiResults;

public class HourlyDetails
{
    [JsonProperty("temperature_2m")] public float[] Temperatures { get; set; } = Array.Empty<float>();
    [JsonProperty("apparent_temperature")] public float[] ApparentTemperatures { get; set; } = Array.Empty<float>();
    [JsonProperty("precipitation_probability")] public float[] PrecipitationProbability { get; set; } = Array.Empty<float>();
}