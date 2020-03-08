using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class CartProductInputType : InputObjectGraphType
    {
        public CartProductInputType()
        {
            Name = "cartProductInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<IntGraphType>>("quantity");
		    Field<NonNullGraphType<FloatGraphType>>("price");
		    Field<NonNullGraphType<IdGraphType>>("cartId");
		    Field<NonNullGraphType<IdGraphType>>("productId");

        }
    }
}
