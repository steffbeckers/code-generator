using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class CountryInputType : InputObjectGraphType
    {
        public CountryInputType()
        {
            Name = "countryInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<IdGraphType>>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");

        }
    }
}
