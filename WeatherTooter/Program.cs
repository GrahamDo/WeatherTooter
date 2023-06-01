namespace WeatherTooter
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                var settings = Settings.Load();
                var setPosition = GetArgumentPosition("--set", args);
                if (setPosition > -1)
                {
                    var settingPosition = setPosition + 1;
                    var valuePosition = setPosition + 2;
                    if (args.Length < valuePosition + 1)
                        throw new ApplicationException("Invalid arguments for --set");

                    settings.SetValueFromArguments(args[settingPosition], args[valuePosition]);
                    settings.Save();
                    return;
                }

                var weatherClient = new WeatherApiClient();
                var weather = await weatherClient.GetForecast(DateTime.Now.Date, settings.LocationLatitude,
                    settings.LocationLongitude, settings.IanaTimeZoneName);
                var conditions = weather.CurrentWeather.GetCurrentConditions();
                var apparentTemperature = weather.Hourly.ApparentTemperatures[weather.CurrentWeather.LocalTime.Hour];
                var forecast = MinMaxForecast.GetFromApiResults(weather, settings.HoursToForecast);
                var template = new TootTemplate(settings.TemplateFile);
                var tootText = template.GetTootText(conditions, settings.LocationName,
                    weather.CurrentWeather.Temperature, apparentTemperature, settings.HoursToForecast,
                    forecast.MinMaxDescriptor, 
                    forecast.MinMaxValue, forecast.PrecipitationChanceArticle,
                    forecast.MaxPrecipitationChance);

                var fakePosition = GetArgumentPosition("--fake", args);
                if (fakePosition > -1)
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

        private static int GetArgumentPosition(string argument, string[] args)
        {
            for (var i = 0; i < args.Length; i++)
                if (args[i].ToLower() == argument.ToLower())
                    return i;

            return -1;
        }
    }
}