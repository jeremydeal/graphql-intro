using BabysFirstGraphQLService.Data.Models;
using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class WeatherForecastType : ObjectGraphType<WeatherForecast>
    {
        public WeatherForecastType()
        {
            Field(x => x.Date).Description("The day of the thing.");
            Field(x => x.TemperatureC).Description("The temp in Celsius.");
            Field(x => x.TemperatureF).Description("The temp in Fahrenheit.");
            Field(x => x.Summary).Description("Unsure...");
            Field<LocationGraphType>(
                "location",
                resolve: context => new WeatherQueryData().GetLocation(context.Source.LocationId)
            );
        }
    }
}

