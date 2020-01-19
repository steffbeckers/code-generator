using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class AddressInputType : InputObjectGraphType
    {
        public AddressInputType()
        {
            Name = "addressInput";
		    Field<NonNullGraphType<StringGraphType>>("street");
		    Field<NonNullGraphType<StringGraphType>>("number");
		    Field<NonNullGraphType<StringGraphType>>("postalCode");
		    Field<NonNullGraphType<StringGraphType>>("city");
		    Field<BooleanGraphType>("primary");
        }
    }
}
