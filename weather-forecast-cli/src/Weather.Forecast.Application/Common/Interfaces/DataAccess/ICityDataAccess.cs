using Weather.Forecast.Domain.Entities.City;

namespace Weather.Forecast.Application.Common.Interfaces.DataAccess
{
    public interface ICityDataAccess
    {
        //Get
        Task<IEnumerable<City>> GetAllCitiesAsync();
    }
}
