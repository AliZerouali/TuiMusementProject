using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Weather.Forecast.Application.Common.Exceptions;
using Weather.Forecast.Application.Common.Interfaces.DataAccess;
using Weather.Forecast.Domain.Entities.City;
using Weather.Forecast.Domain.Entities.WeatherForecast;

namespace Weather.Forecast.Infrastructure.DataAccessLayer
{
    public class WeatherDataAccess : IWeatherDataAccess
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _conf;
        private readonly ILogger<WeatherDataAccess> _log;

        /// <summary>
        /// Weather Data Access Constructor
        /// </summary>
        /// <param name="conf"></param>
        /// <param name="log"></param>
        public WeatherDataAccess(IConfiguration conf, ILogger<WeatherDataAccess> log, HttpClient httpClient)
        {
            _conf = conf;
            _log = log;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get the forecast infos for the each city
        /// </summary>
        /// <param name="cityList"></param>
        /// <returns>List of Weather Object</returns>
        public async Task<IEnumerable<WeatherForecast>> GetAllForecastInfosAsync(IEnumerable<City> cityList)
        {
            List<WeatherForecast> weatherList = new();

            if (!cityList.Any())
            {
                throw new ValidationException("City List must be provided for getting forecast informations");
            }

            int count = 0;

            foreach (City city in cityList)
            {
                if (!string.IsNullOrWhiteSpace(city.Name))
                {
                    string url = _conf.GetValue<string>("WeatherUrl") + $"&q={city.Name}&days=2";

                    var response = await _httpClient.GetAsync(url);

                    var json = await response.Content.ReadAsStringAsync();

                    var weatherForCity = JsonConvert.DeserializeObject<WeatherForecast>(
                        json);

                    if (weatherForCity == null)
                    {
                        throw new NotFoundException(nameof(weatherForCity));
                    }

                    _log.LogInformation($"{++count} - Forecast for the city { city.Name} is well retrieved");

                    weatherList.Add(weatherForCity);
                }
            }

            return weatherList;
        }
    }
}
