using System;
using System.Collections.Generic;
using Weather.Forecast.Domain.Entities.City;
using Weather.Forecast.Domain.Entities.WeatherForecast;
using ForecastObject = Weather.Forecast.Domain.Entities.WeatherForecast.Forecast;

namespace Weather.Forecast.Cli.UnitTests.Common
{
    public static class CommonData
    {
        private static readonly DateTime date = DateTime.Now;
        private static readonly string yesterdayDate = ConvertDateTimeToString(date, -1);
        private static readonly string todayDate = ConvertDateTimeToString(date);
        private static readonly string tomorrowDate = ConvertDateTimeToString(date, 1);
        private static readonly string afterTomorrowDate = ConvertDateTimeToString(date, 2);

        public static string ConvertDateTimeToString(DateTime date, int addingDates = 0)
        {
            return addingDates == 0 ? date.ToString("yyyy-MM-dd") :
                date.AddDays(addingDates).ToString("yyyy-MM-dd");
        }

        public static IEnumerable<City> GetAllCitiesQuery()
        {
            IEnumerable<City> result = new List<City>()
            {
                new City
                {
                    Id = 1,
                    Name = "London",
                },
                new City
                {
                    Id= 2,
                    Name = "Shangai",
                },
                new City
                {
                    Id = 3,
                    Name = "Casablanca",
                },
                new City
                {
                    Id= 4,
                    Name = "Paris",
                }
            };

            return result;
        }

        public static IEnumerable<WeatherForecast> GetAllWeathersQuery()
        {
            IEnumerable<WeatherForecast> result = new List<WeatherForecast>()
            {
                new WeatherForecast
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
                },
                new WeatherForecast
                {
                    Location = new Location
                    {
                        Name = "Shangai",
                        Country = "Venezuela",
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
                                        Text = "Patchy rain possible"
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
                                        Text = "Heavy rain"
                                    }
                                }
                            }
                        }
                    }
                },
                new WeatherForecast
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
                },
                new WeatherForecast
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
                }
            };

            return result;
        }
    }
}
