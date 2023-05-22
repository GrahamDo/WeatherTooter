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
                    settings.LocationLongitude);
                var conditions = weather.CurrentWeather.GetCurrentConditions();
                var apparentTemperature = weather.Hourly.ApparentTemperatures[DateTime.Now.Hour];
                var forecast = MinMaxForecast.GetFromApiResults(weather, settings.HoursToForecast);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}