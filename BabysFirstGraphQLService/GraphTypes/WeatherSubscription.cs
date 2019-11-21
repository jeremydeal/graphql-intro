using BabysFirstGraphQLService.Data.Models;
using BabysFirstGraphQLService.GraphTypes.Messaging;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class WeatherSubscription: ObjectGraphType
    {
        public WeatherSubscription(ForecastMessageService messageService)
        {
            Name = "Subscription";
            AddField(new EventStreamFieldType
            {
                Name = "forecastAdded",
                Type = typeof(ForecastAddedMessageType),
                Resolver = new FuncFieldResolver<ForecastAddedMessage>(c => c.Source as ForecastAddedMessage),
                Subscriber = new EventStreamResolver<ForecastAddedMessage>(c => messageService.GetMessages())
            });
        }
    }
}
