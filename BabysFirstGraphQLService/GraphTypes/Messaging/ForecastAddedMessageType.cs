using BabysFirstGraphQLService.Data.Models;
using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes.Messaging
{
    public class ForecastAddedMessageType: ObjectGraphType<ForecastAddedMessage>
    {
        public ForecastAddedMessageType()
        {
            Field(x => x.Message);
            Field<WeatherForecastType>("forecast");
        }
    }
}
