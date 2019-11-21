using BabysFirstGraphQLService.Data.DAL;
using BabysFirstGraphQLService.Data.Models;
using BabysFirstGraphQLService.GraphTypes.Messaging;
using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class WeatherMutation : ObjectGraphType
    {
        public WeatherMutation(
            ForecastMessageService messageService,
            IWeatherForecastData forecastData
        )
        {
            FieldAsync<WeatherForecastType>(
                "createForecast",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<WeatherForecastInputType>> { Name = "forecast" }),
                resolve: async context =>
                {
                    var forecast = context.GetArgument<WeatherForecast>("forecast");
                    forecast.Location = new Location
                    {
                        LocationId = forecast.LocationId,
                        Name = "locationName" + forecast.LocationId,
                        Zip = "zip" + forecast.LocationId,
                    };

                    // persist
                    await forecastData.CreateWeatherForecast(forecast);

                    messageService.AddForecastAddedMessage(forecast);
                    return forecast;
                });
        }
    }
}
