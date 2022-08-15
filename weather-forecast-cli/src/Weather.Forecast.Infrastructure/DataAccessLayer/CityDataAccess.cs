using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Weather.Forecast.Application.Common.Exceptions;
using Weather.Forecast.Application.Common.Interfaces.DataAccess;
using Weather.Forecast.Domain.Entities.City;

namespace Weather.Forecast.Infrastructure.DataAccessLayer
{
    /// <summary>
    /// City Data Access
    /// </summary>
    public class CityDataAccess : ICityDataAccess
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _conf;

        /// <summary>
        /// City Data Access Constructor
        /// </summary>
        /// <param name="conf"></param>
        public CityDataAccess(IConfiguration conf, HttpClient httpClient)
        {
            _conf = conf;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get the list of all cities from Tui Url
        /// </summary>
        /// <returns>List of Cities</returns>
        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            var tuiUrl = _conf.GetValue<string>("TuiUrl");

            var response = await _httpClient.GetAsync(tuiUrl);

            var json = await response.Content.ReadAsStringAsync();

            var cities = JsonConvert.DeserializeObject<IEnumerable<City>>(
                json);

            if (cities == null)
            {
                throw new NotFoundException(nameof(cities));
            }

            return cities;
        }
    }
}
