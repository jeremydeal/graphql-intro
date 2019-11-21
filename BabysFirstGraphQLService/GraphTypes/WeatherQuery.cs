using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class WeatherQuery : ObjectGraphType
    {
        public WeatherQuery()
        {
            Field<ListGraphType<WeatherForecastType>>(
              "forecasts",
              resolve: context => new WeatherQueryData().GetWeather()
            );

            Field<WeatherForecastType>(
              "forecast",
              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
              {
                  Name = "id"
              }),
              resolve: context => new WeatherQueryData().GetWeather(context.GetArgument<int>("id"))
            );
        }
    }
}

