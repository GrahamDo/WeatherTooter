// ReSharper disable UnusedAutoPropertyAccessor.Global
// All properties need public getters and setters for serialisation to work
namespace WeatherTooter;

internal class MastodonStatus
{
    public string Status { get; set; } = string.Empty;
}