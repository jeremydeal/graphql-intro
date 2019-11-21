using GraphQL;
using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class WeatherSchema : Schema
    {
        public WeatherSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<WeatherQuery>();
            Mutation = resolver.Resolve<WeatherMutation>();
            Subscription = resolver.Resolve<WeatherSubscription>();
        }
    }
}
