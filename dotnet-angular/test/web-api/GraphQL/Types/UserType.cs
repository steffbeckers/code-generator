using GraphQL.Types;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.UserName);
            Field(x => x.Email);
            Field(x => x.FirstName, nullable: true);
            Field(x => x.LastName, nullable: true);
            Field(x => x.Roles, nullable: true, type: typeof(ListGraphType<StringGraphType>));
        }
    }
}
