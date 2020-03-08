using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class OrderInputType : InputObjectGraphType
    {
        public OrderInputType()
        {
            Name = "orderInput";
            Field<IdGraphType>("id");
		    Field<StringGraphType>("number");
		    Field<StringGraphType>("description");
		    Field<FloatGraphType>("totalPrice");

        }
    }
}
