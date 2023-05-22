using NUnit.Framework;
using WeatherTooter.ApiResults;

namespace WeatherTooter.Tests
{
    [TestFixture]
    public class CurrentConditionTests
    {
        [TestCase(0, "clear")]
        [TestCase(1,"mostly clear")]
        [TestCase(2, "partly cloudy")]
        [TestCase(3, "overcast")]
        [TestCase(45, "misty")]
        [TestCase(48, "misty with rime fog")]
        [TestCase(51, "drizzling slightly")]
        [TestCase(53, "drizzling")]
        [TestCase(55, "drizzling heavily")]
        [TestCase(56, "drizzling slightly (freezing)")]
        [TestCase(57, "drizzling heavily (freezing)")]
        [TestCase(61, "raining slightly")]
        [TestCase(63, "raining")]
        [TestCase(65, "raining heavily")]
        [TestCase(66, "raining slightly (freezing)")]
        [TestCase(67, "raining heavily (freezing)")]
        [TestCase(71, "snowing slightly")]
        [TestCase(73, "snowing")]
        [TestCase(75, "snowing heavily")]
        [TestCase(77, "snowing with grains")]
        [TestCase(80, "raining with light showers")]
        [TestCase(81, "raining with showers")]
        [TestCase(82, "raining with heavy showers")]
        [TestCase(85, "snowing with light showers")]
        [TestCase(86, "snowing with heavy showers")]
        [TestCase(95, "raining with thunderstorms")]
        [TestCase(96, "hailing")]
        [TestCase(99, "hailing heavily")]
        public void TestCurrentConditions(byte weatherCode, string currentConditions)
        {
            var sut = new CurrentWeatherDetails { WeatherCode = weatherCode };
            var result = sut.GetCurrentConditions();
            Assert.That(result, Is.EqualTo(currentConditions));
        }
    }
}