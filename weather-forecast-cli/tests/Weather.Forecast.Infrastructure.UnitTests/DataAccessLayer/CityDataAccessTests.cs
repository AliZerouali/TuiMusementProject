using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weather.Forecast.Application.Common.Exceptions;
using Weather.Forecast.Infrastructure.DataAccessLayer;
using Weather.Forecast.Infrastructure.UnitTests.Common;
using Xunit;

namespace Weather.Forecast.Infrastructure.UnitTests.DataAccessLayer
{
    public class CityDataAccessTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly IConfiguration _configuration;

        public CityDataAccessTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>();

            var inMemorySettings = CommonData.GetInMemorySettings();

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        [Fact]
        public async Task GettingAllCititesData_WithSuccess()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""id"": 1, ""name"": ""Porto""}, { ""id"": 100, ""name"": ""Dubai""}]"),
            };

            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var httpClient = new HttpClient(_handlerMock.Object);

            var cityDataAccess = new CityDataAccess(_configuration, httpClient);

            //Act
            var retrieveAllCities = await cityDataAccess.GetAllCitiesAsync();

            //Assert
            Assert.NotNull(retrieveAllCities);
            Assert.Equal(2, retrieveAllCities.Count());
            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task WhenGettingAllCititesData_ThrowsNullException()
        {
            //Arrange
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

            var cityDataAccess = new CityDataAccess(_configuration, httpClient);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => cityDataAccess.GetAllCitiesAsync());
        }
    }
}
