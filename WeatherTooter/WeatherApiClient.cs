using System.Globalization;
using Newtonsoft.Json;
using RestSharp;
using WeatherTooter.ApiResults;

namespace WeatherTooter
{
    internal class WeatherApiClient
    {
        public async Task<ForecastApiResults> GetForecast(DateTime today, float locationLatitude,
            float locationLongitude)
        {
            var tomorrow = today.AddDays(1);
            var restClient = new RestClient("https://api.open-meteo.com/");
            var cultureInvariantLongitude = locationLongitude.ToString("#0.00", CultureInfo.InvariantCulture);
            var cultureInvariantLatitude = locationLatitude.ToString("#0.00", CultureInfo.InvariantCulture);
            var queryString = $"v1/forecast?" +
                              $"latitude={cultureInvariantLatitude}&" +
                              $"longitude={cultureInvariantLongitude}&" +
                              "current_weather=true&" +
                              $"start_date={today:yyyy-MM-dd}&" +
                              $"end_date={tomorrow:yyyy-MM-dd}&" +
                              "hourly=temperature_2m,apparent_temperature," +
                              "precipitation_probability";

            var request = new RestRequest(queryString);
            var response = await restClient.GetAsync(request);
            if (response?.Content == null)
                throw new ApplicationException("Empty response from Weather API Client");

            var results = JsonConvert.DeserializeObject<ForecastApiResults>(response.Content);
            if (results == null)
                throw new ApplicationException("Can't deserialise Weather Content");

            return results;
        }
    }
}
