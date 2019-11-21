using BabysFirstGraphQLService.Data.DAL;
using GraphQL.Types;

namespace BabysFirstGraphQLService.GraphTypes
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery()
        {
            Field<ListGraphType<UserType>>(
              "users",
              resolve: context => new UserData().GetUsers()
            );

            Field<UserType>(
              "user",
              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
              {
                  Name = "id"
              }),
              resolve: context => new UserData().GetUser(context.GetArgument<int>("id"))
            );
        }
    }
}

