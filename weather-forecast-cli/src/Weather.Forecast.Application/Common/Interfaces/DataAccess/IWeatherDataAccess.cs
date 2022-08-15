using Weather.Forecast.Domain.Entities.City;
using Weather.Forecast.Domain.Entities.WeatherForecast;

namespace Weather.Forecast.Application.Common.Interfaces.DataAccess
{
    public interface IWeatherDataAccess
    {
        //Get
        Task<IEnumerable<WeatherForecast>> GetAllForecastInfosAsync(IEnumerable<City> cityList);
    }
}
