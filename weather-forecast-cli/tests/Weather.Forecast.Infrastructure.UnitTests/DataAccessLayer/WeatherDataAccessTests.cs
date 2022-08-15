using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weather.Forecast.Application.Common.Exceptions;
using Weather.Forecast.Domain.Entities.City;
using Weather.Forecast.Infrastructure.DataAccessLayer;
using Weather.Forecast.Infrastructure.UnitTests.Common;
using Xunit;

namespace Weather.Forecast.Infrastructure.UnitTests.DataAccessLayer
{
    public class WeatherDataAccessTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly Mock<ILogger<WeatherDataAccess>> _logMock;
        private readonly IConfiguration _configuration;

        public WeatherDataAccessTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>();
            _logMock = new Mock<ILogger<WeatherDataAccess>>();

            var inMemorySettings = CommonData.GetInMemorySettings();

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        [Fact]
        public async Task GettingAllForecastData_WithSuccess()
        {
            //Arrange
            var cities = CommonData.GetAllCitiesQuery();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(CommonData.GetForecastInfosObject()),
            };

            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var httpClient = new HttpClient(_handlerMock.Object);

            ILogger<WeatherDataAccess> logger = _logMock.Object;

            var weatherDataAccess = new WeatherDataAccess(_configuration, logger, httpClient);

            //Act
            var retrieveAllForecastInfos = await weatherDataAccess.GetAllForecastInfosAsync(cities);

            //Assert
            Assert.NotNull(retrieveAllForecastInfos);
            Assert.Equal(cities.Count(), retrieveAllForecastInfos.Count());
            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(cities.Count()),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GettingAllForecastData_WenCitiesHaveSomeNullNames()
        {
            //Arrange
            var cities = CommonData.GetCitiesWithSomeNullNamesQuery();
            var countCitiesNotNullName = cities.Where(x => !string.IsNullOrWhiteSpace(x.Name)).Count();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(CommonData.GetForecastInfosObject()),
            };

            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var httpClient = new HttpClient(_handlerMock.Object);

            ILogger<WeatherDataAccess> logger = _logMock.Object;

            var weatherDataAccess = new WeatherDataAccess(_configuration, logger, httpClient);

            //Act
            var retrieveAllForecastInfos = await weatherDataAccess.GetAllForecastInfosAsync(cities);

            //Assert
            Assert.NotNull(retrieveAllForecastInfos);
            Assert.Equal(countCitiesNotNullName, retrieveAllForecastInfos.Count());
            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(countCitiesNotNullName),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task WhenHavingEmptyParameters_ThrowException()
        {
            //Arrange
            IEnumerable<City> cities = new List<City>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(""),
            };

            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var httpClient = new HttpClient(_handlerMock.Object);

            ILogger<WeatherDataAccess> logger = _logMock.Object;

            var weatherDataAccess = new WeatherDataAccess(_configuration, logger, httpClient);

            //Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => weatherDataAccess.GetAllForecastInfosAsync(cities));
        }

        [Fact]
        public async Task WhenForecastNotFound_ThrowException()
        {
            //Arrange
            var cities = CommonData.GetAllCitiesQuery();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(""),
            };

            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var httpClient = new HttpClient(_handlerMock.Object);

            ILogger<WeatherDataAccess> logger = _logMock.Object;

            var weatherDataAccess = new WeatherDataAccess(_configuration, logger, httpClient);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => weatherDataAccess.GetAllForecastInfosAsync(cities));
        }
    }
}
