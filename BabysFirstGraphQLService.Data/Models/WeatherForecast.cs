using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace BabysFirstGraphQLService.Data.Models
{
    public class WeatherForecast
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string WeatherForecastId { get; set; }
        public DateTime Date { get; set; }
        public double TemperatureC { get; set; }
        public double TemperatureF => 32 + TemperatureC / 0.5556;
        public string Summary { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; } 
    }
}
