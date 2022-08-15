using System;
using System.Collections.Generic;
using Weather.Forecast.Domain.Entities.City;

namespace Weather.Forecast.Infrastructure.UnitTests.Common
{
    public static class CommonData
    {
        private static readonly DateTime date = DateTime.Now;
        private static readonly string todayDate = ConvertDateTimeToString(date);
        private static readonly string tomorrowDate = ConvertDateTimeToString(date, 1);

        public static string GetForecastInfosObject()
        {
            string result = @"{ ""location"": { ""name"": ""London"",
                                                ""country"" : ""United Kingdom"",
                                                ""localtime"" :"" " + todayDate + @" "",
                                              },
                                ""forecast"": {
                                      ""forecastday"" : [ { ""date"" : "" " + todayDate + @" "",
                                                            ""day"" : {
                                                                   ""condition"" : {
                                                                                 ""text"" : ""Partly cloudy""
                                                                                   }
                                                                       }
                                                           },
                                                           { ""date"" : "" " + tomorrowDate + @" "",
                                                             ""day"" : {
                                                                    ""condition"" : {
                                                                                  ""text"" : ""Sunny""
                                                                                    }
                                                                        }
                                                           }]
                                                }
                                }";

            return result;
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
                }
            };

            return result;
        }

        public static IEnumerable<City> GetCitiesWithSomeNullNamesQuery()
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
                    Name = "",
                },
                new City
                {
                    Id = 3
                }
            };

            return result;
        }

        public static string ConvertDateTimeToString(DateTime date, int addingDates = 0)
        {
            return addingDates == 0 ? date.ToString("yyyy-MM-dd") :
                date.AddDays(addingDates).ToString("yyyy-MM-dd");
        }

        public static Dictionary<string, string> GetInMemorySettings()
        {
            Dictionary<string, string> settings = new()
            {
                { "TuiUrl", "https://api.musement.com/api/v3/cities" },
                { "WeatherUrl", "http://api.weatherapi.com/v1/forecast.json?key=66451d05af3b4fae985163644220908" },
            };

            return settings;
        }
    }
}
