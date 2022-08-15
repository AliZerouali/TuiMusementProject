namespace Weather.Forecast.Domain.Entities.WeatherForecast
{
    /// <summary>
    /// Location Object
    /// </summary>
    public class Location
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? LocalTime { get; set; }
    }
}
