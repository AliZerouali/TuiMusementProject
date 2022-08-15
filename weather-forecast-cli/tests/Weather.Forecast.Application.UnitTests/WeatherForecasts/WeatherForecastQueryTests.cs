using Weather.Forecast.Application.UnitTests.Common;
using Weather.Forecast.Application.WeatherForecasts.Queries;
using Weather.Forecast.Domain.Entities.WeatherForecast;
using Xunit;

namespace Weather.Forecast.Application.UnitTests.WeatherForecasts
{
    public class WeatherForecastQueryTests
    {
        [Fact]
        public void GettingWeatherForecast_WhenCityHasSameLocalTodayDate()
        {
            //Arrange
            WeatherForecast weatherForecast = CommonData.GetWeatherForecastWithTodayDate();

            string? expectedTodayWeather = weatherForecast.Forecast?.Forecastday?
                .Find(x => x.Date == CommonData.todayDate)?.Day?.Condition?.Text;

            string? expectedTomorrowWeather = weatherForecast.Forecast?.Forecastday?
                .Find(x => x.Date == CommonData.tomorrowDate)?.Day?.Condition?.Text;

            //Act
            var weatherForecastString = WeatherForecastQuery.GetWeatherForecastString(weatherForecast);

            //Assert
            var actualWeather = weatherForecastString.Split('-');
            Assert.NotNull(weatherForecastString);
            if (!string.IsNullOrWhiteSpace(expectedTodayWeather) && !string.IsNullOrWhiteSpace(expectedTomorrowWeather))
            {
                Assert.True(actualWeather[0]?.Contains(expectedTodayWeather));
                Assert.True(actualWeather[1]?.Contains(expectedTomorrowWeather));
            }
        }

        [Fact]
        public void GettingWeatherForecast_WhenCityHasLocalTime_YesterdayDate()
        {
            //Arrange
            WeatherForecast weatherForecast = CommonData.GetWeatherForecastWithYesterdayDate();

            string? expectedTodayWeather = weatherForecast.Forecast?.Forecastday?
                .Find(x => x.Date == CommonData.yesterdayDate)?.Day?.Condition?.Text;

            string? expectedTomorrowWeather = weatherForecast.Forecast?.Forecastday?
                .Find(x => x.Date == CommonData.todayDate)?.Day?.Condition?.Text;

            //Act
            var weatherForecastString = WeatherForecastQuery.GetWeatherForecastString(weatherForecast);

            //Assert
            var actualWeather = weatherForecastString.Split('-');
            Assert.NotNull(weatherForecastString);
            if (!string.IsNullOrWhiteSpace(expectedTodayWeather) && !string.IsNullOrWhiteSpace(expectedTomorrowWeather))
            {
                Assert.True(actualWeather[0]?.Contains(expectedTodayWeather));
                Assert.True(actualWeather[1]?.Contains(expectedTomorrowWeather));
            }
        }

        [Fact]
        public void GettingWeatherForecast_WhenCityHasLocalTime_TomorrowDate()
        {
            //Arrange
            WeatherForecast weatherForecast = CommonData.GetWeatherForecastWithTodayDate();

            string? expectedTodayWeather = weatherForecast.Forecast?.Forecastday?
                .Find(x => x.Date == CommonData.tomorrowDate)?.Day?.Condition?.Text;

            string? expectedTomorrowWeather = weatherForecast.Forecast?.Forecastday?
                .Find(x => x.Date == CommonData.afterTomorrowDate)?.Day?.Condition?.Text;

            //Act
            var weatherForecastString = WeatherForecastQuery.GetWeatherForecastString(weatherForecast);

            //Assert
            var actualWeather = weatherForecastString.Split('-');
            Assert.NotNull(weatherForecastString);
            if (!string.IsNullOrWhiteSpace(expectedTodayWeather) && !string.IsNullOrWhiteSpace(expectedTomorrowWeather))
            {
                Assert.True(actualWeather[0]?.Contains(expectedTodayWeather));
                Assert.True(actualWeather[1]?.Contains(expectedTomorrowWeather));
            }
        }
    }
}
