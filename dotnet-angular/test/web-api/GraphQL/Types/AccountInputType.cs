using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class AccountInputType : InputObjectGraphType
    {
        public AccountInputType()
        {
            Name = "accountInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<StringGraphType>("website");
		    Field<StringGraphType>("telephone");
		    Field<StringGraphType>("email");
		    Field<IdGraphType>("parentAccountId");

        }
    }
}
