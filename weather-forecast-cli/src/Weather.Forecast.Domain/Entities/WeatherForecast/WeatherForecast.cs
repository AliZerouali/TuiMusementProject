namespace Weather.Forecast.Domain.Entities.WeatherForecast
{
    /// <summary>
    /// Weather Object
    /// </summary>
    public class WeatherForecast
    {
        public Location? Location { get; set; }
        public Forecast? Forecast { get; set; }
    }
}
