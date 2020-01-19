using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class ContactInputType : InputObjectGraphType
    {
        public ContactInputType()
        {
            Name = "contactInput";
		    Field<NonNullGraphType<StringGraphType>>("firstName");
		    Field<NonNullGraphType<StringGraphType>>("lastName");
		    Field<StringGraphType>("website");
		    Field<StringGraphType>("telephone");
		    Field<StringGraphType>("email");
        }
    }
}
