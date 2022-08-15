namespace Weather.Forecast.Domain.Entities.WeatherForecast
{
    /// <summary>
    /// Forecast Day Object
    /// </summary>
    public class ForecastDay
    {
        public string? Date { get; set; }
        public Day? Day { get; set; }
    }
}
