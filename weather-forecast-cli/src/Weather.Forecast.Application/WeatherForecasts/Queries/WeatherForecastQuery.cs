using Weather.Forecast.Application.Common.FormattingAndManipulation;
using Weather.Forecast.Domain.Entities.WeatherForecast;

namespace Weather.Forecast.Application.WeatherForecasts.Queries
{
    public class WeatherForecastQuery
    {
        /// <summary>
        /// Get the forecast infos for one city
        /// </summary>
        /// <param name="weather"></param>
        /// <returns>string containing the city name and two days forecast infos</returns>
        public static string GetWeatherForecastString(WeatherForecast weather)
        {
            DateTime date = DateTime.Now;
            string yesterday = Conversions.ConvertDateTimeToString(date, -1);
            string today = Conversions.ConvertDateTimeToString(date);
            string tomorrow = Conversions.ConvertDateTimeToString(date, 1);
            string afterTomorrow = Conversions.ConvertDateTimeToString(date, 2);

            string? todayWeather = weather.Forecast?.Forecastday?.Find(x => x.Date == today)?.Day?.Condition?.Text;
            string? tomorrowWeather = weather.Forecast?.Forecastday?.Find(x => x.Date == tomorrow)?.Day?.Condition?.Text;
            string? cityName = weather?.Location?.Name;

            //Verify if the City Local Time is different from Today Local Time and it's equal to tomorrow
            bool isCityLocalTimeisTomorrow = weather?.Location?.LocalTime == null ? false : weather.Location.LocalTime.Contains(tomorrow);

            //Verify if the City Local Time is different from Today Local Time and it's equal to yesterday
            bool isCityLocalTimeisYesterday = weather?.Location?.LocalTime == null ? false : weather.Location.LocalTime.Contains(yesterday);

            if (isCityLocalTimeisTomorrow)
            {
                todayWeather = tomorrowWeather;
                tomorrowWeather = weather?.Forecast?.Forecastday?.Find(x => x.Date == afterTomorrow)?.Day?.Condition?.Text;
            }

            if (isCityLocalTimeisYesterday)
            {
                todayWeather = weather?.Forecast?.Forecastday?.Find(x => x.Date == yesterday)?.Day?.Condition?.Text;
                tomorrowWeather = weather?.Forecast?.Forecastday?.Find(x => x.Date == today)?.Day?.Condition?.Text;
            }

            string weatherCityString = $@"Processed city {cityName} | {todayWeather} - {tomorrowWeather}";

            return weatherCityString;
        }
    }
}
