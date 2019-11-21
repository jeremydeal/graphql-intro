using BabysFirstGraphQLService.Data.Models;
using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class LocationGraphType: ObjectGraphType<Location>
    {
        public LocationGraphType()
        {
            Field(x => x.LocationId);
            Field(x => x.Name);
            Field(x => x.Zip);
        }
    }
}