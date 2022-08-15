using System;
using System.Collections.Generic;
using Weather.Forecast.Domain.Entities.WeatherForecast;
using ForecastObject = Weather.Forecast.Domain.Entities.WeatherForecast.Forecast;

namespace Weather.Forecast.Application.UnitTests.Common
{
    public class CommonData
    {
        private static readonly DateTime date = DateTime.Now;
        public static readonly string yesterdayDate = ConvertDateTimeToString(date, -1);
        public static readonly string todayDate = ConvertDateTimeToString(date);
        public static readonly string tomorrowDate = ConvertDateTimeToString(date, 1);
        public static readonly string afterTomorrowDate = ConvertDateTimeToString(date, 2);

        public static string ConvertDateTimeToString(DateTime date, int addingDates = 0)
        {
            return addingDates == 0 ? date.ToString("yyyy-MM-dd") :
                date.AddDays(addingDates).ToString("yyyy-MM-dd");
        }

        public static WeatherForecast GetWeatherForecastWithYesterdayDate()
        {
            WeatherForecast result = new WeatherForecast
            {
                Location = new Location
                {
                    Name = "Paris",
                    Country = "France",
                    LocalTime = yesterdayDate
                },
                Forecast = new ForecastObject
                {
                    Forecastday = new List<ForecastDay>
                        {
                            new ForecastDay
                            {
                                Date = yesterdayDate,
                                Day = new Day
                                {
                                    Condition = new Condition
                                    {
                                        Text = "Patchy rain possible"
                                    }
                                }
                            },
                            new ForecastDay
                            {
                                Date = todayDate,
                                Day = new Day
                                {
                                    Condition = new Condition
                                    {
                                        Text = "Sunny"
                                    }
                                }
                            }
                        }
                }
            };

            return result;
        }

        public static WeatherForecast GetWeatherForecastWithTomorrowDate()
        {
            WeatherForecast result = new WeatherForecast
            {
                Location = new Location
                {
                    Name = "Casablanca",
                    Country = "Morocco",
                    LocalTime = tomorrowDate
                },
                Forecast = new ForecastObject
                {
                    Forecastday = new List<ForecastDay>
                        {
                            new ForecastDay
                            {
                                Date = tomorrowDate,
                                Day = new Day
                                {
                                    Condition = new Condition
                                    {
                                        Text = "Sunny"
                                    }
                                }
                            },
                            new ForecastDay
                            {
                                Date = afterTomorrowDate,
                                Day = new Day
                                {
                                    Condition = new Condition
                                    {
                                        Text = "Heavy rain"
                                    }
                                }
                            }
                        }
                }
            };

            return result;
        }

        public static WeatherForecast GetWeatherForecastWithTodayDate()
        {
            WeatherForecast result = new WeatherForecast
            {
                Location = new Location
                {
                    Name = "London",
                    Country = "United Kingdom",
                    LocalTime = todayDate
                },
                Forecast = new ForecastObject
                {
                    Forecastday = new List<ForecastDay>
                        {
                            new ForecastDay
                            {
                                Date = todayDate,
                                Day = new Day
                                {
                                    Condition = new Condition
                                    {
                                        Text = "Partly cloudy"
                                    }
                                }
                            },
                            new ForecastDay
                            {
                                Date = tomorrowDate,
                                Day = new Day
                                {
                                    Condition = new Condition
                                    {
                                        Text = "Sunny"
                                    }
                                }
                            }
                        }
                }
            };

            return result;
        }
    }
}
