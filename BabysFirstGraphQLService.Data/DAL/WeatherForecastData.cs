using BabysFirstGraphQLService.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BabysFirstGraphQLService.Data.DAL
{
    public interface IWeatherForecastData
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
        Task CreateWeatherForecast(WeatherForecast model);
    }

    public class WeatherForecastData : IWeatherForecastData
    {
        private readonly IMongoClient _mongoClient;
        private IMongoDatabase _db => _mongoClient.GetDatabase("test");
        private IMongoCollection<WeatherForecast> _table => _db.GetCollection<WeatherForecast>("forecasts");

        public WeatherForecastData(IMongoClient client)
        {
            _mongoClient = client;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            var cursor = await _table.FindAsync(new BsonDocument());
            return cursor.ToList();
        }

        public async Task CreateWeatherForecast(WeatherForecast model)
        {
            try
            {
                //bool isMongoLive = _db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
                //var currentTask = await _table.FindAsync(new BsonDocument());
                //var current = currentTask.ToList();
                await _table.InsertOneAsync(model);
            }
            catch (Exception ex)
            {
                var hi = ex.Message;
            }
        }
    }
}
