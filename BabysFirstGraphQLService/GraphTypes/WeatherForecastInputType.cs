using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class WeatherForecastInputType: InputObjectGraphType
    {
        public WeatherForecastInputType()
        {
            Name = "forecastInput";
            Field<NonNullGraphType<IntGraphType>>("locationId");
            Field<NonNullGraphType<DateGraphType>>("date");
            Field<NonNullGraphType<FloatGraphType>>("temperatureC");
            Field<NonNullGraphType<StringGraphType>>("summary");
        }
    }
}