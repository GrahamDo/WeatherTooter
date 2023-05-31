using NUnit.Framework;
using WeatherTooter.ApiResults;

namespace WeatherTooter.Tests
{
    [TestFixture]
    public class PrecipitationChanceArticleTests
    {
        [TestCase(0, "a")]
        [TestCase(5, "a")]
        [TestCase(8, "an")]
        [TestCase(11, "an")]
        [TestCase(18, "an")]
        [TestCase(80, "an")]
        [TestCase(81, "an")]
        [TestCase(82, "an")]
        [TestCase(83, "an")]
        [TestCase(84, "an")]
        [TestCase(85, "an")]
        [TestCase(86, "an")]
        [TestCase(87, "an")]
        [TestCase(88, "an")]
        [TestCase(89, "an")]
        [TestCase(90, "a")]

        public void TestPrecipitationChanceArticle(float maxPrecipitationChance, string expected)
        {
            var currentDetails = new CurrentWeatherDetails();
            var hourlyDetails = new HourlyDetails
            {
                Temperatures = new[] // 48 elements for 48 hours
                    { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f },

                PrecipitationProbability = new[]
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, maxPrecipitationChance, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            var apiResult = new ForecastApiResults
            {
                CurrentWeather = currentDetails,
                Hourly = hourlyDetails
            };

            var forecast = MinMaxForecast.GetFromApiResults(apiResult, 24);
            Assert.That(forecast.PrecipitationChanceArticle, Is.EqualTo(expected));
        }
    }
}
