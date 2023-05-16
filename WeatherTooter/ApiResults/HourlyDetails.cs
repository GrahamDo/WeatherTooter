using Newtonsoft.Json;

namespace WeatherTooter.ApiResults;

internal class HourlyDetails
{
    [JsonProperty("temperature_2m")] public float[] Temperatures { get; set; }
    [JsonProperty("apparent_temperature")] public float[] ApparentTemperatures { get; set; }
    [JsonProperty("precipitation_probability")] public float[] PrecipitationProbability { get; set; }

    public HourlyDetails()
    {
        Temperatures = Array.Empty<float>();
        ApparentTemperatures = Array.Empty<float>();
        PrecipitationProbability = Array.Empty<float>();
    }
}