using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class CartInputType : InputObjectGraphType
    {
        public CartInputType()
        {
            Name = "cartInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");

            // To create a link with Product directly on create of Cart.
            //Field<IdGraphType>("productId");
            //Field<NonNullGraphType<IntGraphType>>("productQuantity");
            //Field<NonNullGraphType<FloatGraphType>>("productPrice");
        }
    }
}
