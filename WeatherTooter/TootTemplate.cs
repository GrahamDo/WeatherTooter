namespace WeatherTooter;

internal class TootTemplate
{
    private readonly string _templateFileName;

    public TootTemplate(string templateFileName)
    {
        _templateFileName = templateFileName;
    }

    public string GetTootText(string currentConditions, string locationName, 
        float currentTemperatureCelsius, float apparentTemperatureCelsius, 
        byte hoursToForecast, string minimumOrMaximum, float forecastTemperatureCelsius, 
        string precipitationChanceArticle, float precipitationChance)
    {
        var temperatureText = $"{currentTemperatureCelsius:#0}°C";
        var apparentTemperatureText = $"{apparentTemperatureCelsius:#0}°C";
        var timePeriodText = hoursToForecast + (hoursToForecast == 1 ? " hour" : " hours");
        var forecastTemperatureText = $"{forecastTemperatureCelsius:#0}°C";
        var precipitationChanceText = $"{precipitationChance:#0}%";
        var locationNameNoSpaces = locationName.Replace(" ", "");

        var template = Load();
        return template.Replace("{Conditions}", currentConditions)
            .Replace("{Location}", locationName)
            .Replace("{Temperature}", temperatureText)
            .Replace("{ApparentTemperature}", apparentTemperatureText)
            .Replace("{TimePeriod}", timePeriodText)
            .Replace("{MinMax}", minimumOrMaximum)
            .Replace("{ForecastTemperature}", forecastTemperatureText)
            .Replace("{PrecipitationChance}", precipitationChanceText)
            .Replace("{PrecipitationChanceArticle}", precipitationChanceArticle)
            .Replace("{LocationNoSpaces}", locationNameNoSpaces);
    }

    private string Load()
    {
        if (!File.Exists(_templateFileName))
            throw new ApplicationException($"{_templateFileName} not found");

        var text = File.ReadAllText(_templateFileName);
        return text;
    }
}