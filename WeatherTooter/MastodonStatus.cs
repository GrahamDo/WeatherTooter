// ReSharper disable UnusedAutoPropertyAccessor.Global
// All properties need public getters and setters for serialisation to work

using Newtonsoft.Json;

namespace WeatherTooter;

internal class MastodonStatus
{
    [JsonProperty("status")] public string Status { get; set; } = string.Empty;
}