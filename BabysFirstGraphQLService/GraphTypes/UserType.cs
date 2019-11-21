using BabysFirstGraphQLService.Data.Models;
using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.UserId).Description("PK!");
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Email);
            Field(x => x.CreatedAt);
        }
    }
}

