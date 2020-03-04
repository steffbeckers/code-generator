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
		    Field<StringGraphType>("email");
		    Field<StringGraphType>("telephone");
		    Field<StringGraphType>("fax");
		    Field<StringGraphType>("website");
		    Field<StringGraphType>("vATNumber");
		    Field<StringGraphType>("description");
		    Field<IdGraphType>("addressId");
		    Field<IdGraphType>("parentAccountId");
		    Field<IdGraphType>("billingAccountId");
		    Field<IdGraphType>("relationTypeId");
		    Field<IdGraphType>("primaryContactId");

        }
    }
}
