using BabysFirstGraphQLService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class WeatherQueryData
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> GetWeather()
        {
            return Enumerable.Range(1, 5).Select(GetWeather);
        }

        public WeatherForecast GetWeather(int id)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(id),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                LocationId = rng.Next(1, 3),
            };
        }

        public IEnumerable<Location> GetLocation()
        {
            return Enumerable.Range(1, 2).Select(GetLocation);
        }

        public Location GetLocation(int id)
        {
            return new Location
            {
                LocationId = id,
                Name = "Location" + id,
                Zip = "8434" + id
            };
        }
    }
}

