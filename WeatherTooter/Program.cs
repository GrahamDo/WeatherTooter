namespace WeatherTooter
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                var settings = Settings.Load();
                if (args.Length == 3 && args[0].ToLower() == "--set")
                {
                    settings.SetValueFromArguments(args[1], args[2]);
                    settings.Save();
                    return;
                }

                var weatherClient = new WeatherApiClient();
                var weather = await weatherClient.GetForecast(DateTime.Now.Date, settings.LocationLatitude,
                    settings.LocationLongitude, settings.IanaTimeZoneName);
                var conditions = weather.CurrentWeather.GetCurrentConditions();
                var apparentTemperature = weather.Hourly.ApparentTemperatures[weather.CurrentWeather.LocalTime.Hour];
                var forecast = MinMaxForecast.GetFromApiResults(weather, settings.HoursToForecast);
                var template = new TootTemplate();
                var tootText = template.GetTootText(conditions, settings.LocationName,
                    weather.CurrentWeather.Temperature, apparentTemperature, settings.HoursToForecast,
                    forecast.MinMaxDescriptor, forecast.MinMaxValue, forecast.MaxPrecipitationChance);

                if (args.Length == 1 && args[0].ToLower() == "--fake")
                {
                    Console.WriteLine("Not posting to Mastodon. --fake detected.");
                    Console.WriteLine("The following WOULD be posted:");
                    Console.WriteLine();
                    Console.WriteLine(tootText);
                }
                else
                {
                    var mastodon = new MastodonApiClient();
                    await mastodon.Post(settings.MastodonInstanceUrl, settings.MastodonToken, tootText);
                }
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}