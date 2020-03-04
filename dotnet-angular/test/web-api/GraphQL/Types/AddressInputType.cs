using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class AddressInputType : InputObjectGraphType
    {
        public AddressInputType()
        {
            Name = "addressInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<IdGraphType>>("id");
		    Field<StringGraphType>("street");
		    Field<StringGraphType>("number");
		    Field<StringGraphType>("city");
		    Field<StringGraphType>("state");
		    Field<StringGraphType>("postalCode");
		    Field<decimal>("latitude");
		    Field<decimal>("longitude");
		    Field<IdGraphType>("countryId");

        }
    }
}
