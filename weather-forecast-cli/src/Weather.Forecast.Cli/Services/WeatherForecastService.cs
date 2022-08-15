using Microsoft.Extensions.Logging;
using Weather.Forecast.Application.Common.Interfaces.DataAccess;
using Weather.Forecast.Application.WeatherForecasts.Queries;
using Weather.Forecast.Domain.Entities.WeatherForecast;

namespace Weather.Forecast.Cli.Services
{
    /// <summary>
    /// Weather Forecast Service
    /// </summary>
    public class WeatherForecastService
    {
        private readonly ICityDataAccess _cityDataAccess;
        private readonly IWeatherDataAccess _weatherDataAccess;
        private readonly ILogger<WeatherForecastService> _log;

        /// <summary>
        /// Weather Forecast Service Constructor with parameters
        /// </summary>
        /// <param name="cityDataAccess"></param>
        /// <param name="weatherDataAccess"></param>
        /// <param name="log"></param>
        public WeatherForecastService(ICityDataAccess cityDataAccess, IWeatherDataAccess weatherDataAccess, ILogger<WeatherForecastService> log)
        {
            _cityDataAccess = cityDataAccess;
            _weatherDataAccess = weatherDataAccess;
            _log = log;
        }

        /// <summary>
        /// Get the forecast informations for all cities
        /// </summary>
        public async Task GetAllForecastInfosAsync()
        {
            try
            {
                _log.LogInformation("Start Getting All Cities From Tui Musement Url");
                var cities = await _cityDataAccess.GetAllCitiesAsync();

                _log.LogInformation("Start Getting All Cities Forecast Informations From Weather Url");
                var weathers = await _weatherDataAccess.GetAllForecastInfosAsync(cities);

                IEnumerable<WeatherForecast> weatherOrdredByName = weathers.OrderBy(x => x.Location?.Name);

                foreach (WeatherForecast weather in weatherOrdredByName)
                {
                    string weatherCityString = WeatherForecastQuery.GetWeatherForecastString(weather);

                    Console.WriteLine(weatherCityString);
                }

                _log.LogInformation($"Getting {weatherOrdredByName.Count()} Cities Forecast Informations with Success");
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw;
            }
        }
    }
}
