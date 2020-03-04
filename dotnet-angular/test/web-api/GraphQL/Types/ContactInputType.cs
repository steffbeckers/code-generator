using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class ContactInputType : InputObjectGraphType
    {
        public ContactInputType()
        {
            Name = "contactInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("lastName");
		    Field<NonNullGraphType<IdGraphType>>("accountId");
		    Field<StringGraphType>("firstName");
		    Field<StringGraphType>("jobTitle");
		    Field<StringGraphType>("email");
		    Field<StringGraphType>("telephone");
		    Field<StringGraphType>("mobilePhone");
		    Field<StringGraphType>("gender");

        }
    }
}
