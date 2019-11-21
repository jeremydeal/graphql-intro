using BabysFirstGraphQLService.Data.Models;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BabysFirstGraphQLService.GraphTypes.Messaging
{
    public class ForecastMessageService
    {
        private readonly ISubject<ForecastAddedMessage> _messageStream = new ReplaySubject<ForecastAddedMessage>(1);

        public ForecastAddedMessage AddForecastAddedMessage(WeatherForecast forecast)
        {
            var message = new ForecastAddedMessage
            {
                Forecast = forecast,
                Message = "hi!!!!"
            };
            _messageStream.OnNext(message);
            return message;
        }

        public IObservable<ForecastAddedMessage> GetMessages()
        {
            return _messageStream.AsObservable();
        }
    }
}
