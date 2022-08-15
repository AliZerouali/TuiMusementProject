namespace Weather.Forecast.Domain.Entities.WeatherForecast
{
    /// <summary>
    /// Forecast Object
    /// </summary>
    public class Forecast
    {
        public List<ForecastDay>? Forecastday { get; set; }
    }
}
