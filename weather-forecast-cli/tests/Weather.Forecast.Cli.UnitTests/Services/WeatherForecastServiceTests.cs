using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Weather.Forecast.Application.Common.Interfaces.DataAccess;
using Weather.Forecast.Cli.Services;
using Weather.Forecast.Cli.UnitTests.Common;
using Xunit;

namespace Weather.Forecast.Cli.UnitTests.Services
{
    public class WeatherForecastServiceTests
    {
        private readonly Mock<ICityDataAccess> _cityDataAccess;
        private readonly Mock<IWeatherDataAccess> _weatherDataAccess;
        private readonly Mock<ILogger<WeatherForecastService>> _logMock;

        public WeatherForecastServiceTests()
        {
            _logMock = new Mock<ILogger<WeatherForecastService>>();
            _weatherDataAccess = new Mock<IWeatherDataAccess>();
            _cityDataAccess = new Mock<ICityDataAccess>();
        }

        [Fact]
        public async Task GettingAllForecastOutPut_WithSuccess()
        {
            //Arrange
            var cities = CommonData.GetAllCitiesQuery();
            var weathers = CommonData.GetAllWeathersQuery();

            _cityDataAccess.Setup(x => x.GetAllCitiesAsync()).ReturnsAsync(cities).Verifiable();
            _weatherDataAccess.Setup(x => x.GetAllForecastInfosAsync(cities)).ReturnsAsync(weathers).Verifiable();

            ILogger<WeatherForecastService> logger = _logMock.Object;
            ICityDataAccess cityDataAccess = _cityDataAccess.Object;
            IWeatherDataAccess weatherDataAccess = _weatherDataAccess.Object;

            var weatherService = new WeatherForecastService(cityDataAccess, weatherDataAccess, logger);

            //Act
            await weatherService.GetAllForecastInfosAsync();

            //Assert
            _cityDataAccess.Verify();
            _weatherDataAccess.Verify();
        }
    }
}
