using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class OrderStateInputType : InputObjectGraphType
    {
        public OrderStateInputType()
        {
            Name = "orderStateInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<StringGraphType>("displayName");

        }
    }
}
