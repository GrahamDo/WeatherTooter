using System.Globalization;
using System.Net;
using Newtonsoft.Json;
using WeatherTooter.ApiResults;

namespace WeatherTooter
{
    internal class WeatherApiClient(HttpClientFactory clientFactory)
    {
        private readonly HttpClient _client = clientFactory.GetClient();
        
        public async Task<ForecastApiResults> GetForecast(DateTime today, float locationLatitude,
            float locationLongitude, string ianaTimeZoneName)
        {
            if (locationLatitude == float.MinValue)
                throw new ApplicationException("Missing location latitude");
            if (locationLongitude == float.MinValue)
                throw new ApplicationException("Missing location longitude");
            if (string.IsNullOrEmpty(ianaTimeZoneName))
                throw new ApplicationException("Missing time zone name");

            var tomorrow = today.AddDays(1);
            var cultureInvariantLongitude = locationLongitude.ToString("#0.00", CultureInfo.InvariantCulture);
            var cultureInvariantLatitude = locationLatitude.ToString("#0.00", CultureInfo.InvariantCulture);
            var url = $"https://api.open-meteo.com/v1/forecast?" +
                              $"latitude={cultureInvariantLatitude}&" +
                              $"longitude={cultureInvariantLongitude}&" +
                              "current_weather=true&" +
                              $"timezone={ianaTimeZoneName}&" +
                              $"start_date={today:yyyy-MM-dd}&" +
                              $"end_date={tomorrow:yyyy-MM-dd}&" +
                              "hourly=temperature_2m,apparent_temperature," +
                              "precipitation_probability";
            
            try
            {
                var response = await _client.GetStringAsync(url);
                var results = JsonConvert.DeserializeObject<ForecastApiResults>(response);
                if (results == null)
                    throw new ApplicationException("Can't deserialise Weather Content");

                return results;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                    throw new ApplicationException("Error contacting Weather API. Could be an invalid time zone name");

                throw;
            }
        }
    }
}
