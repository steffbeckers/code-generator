using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class ProductDetailInputType : InputObjectGraphType
    {
        public ProductDetailInputType()
        {
            Name = "productDetailInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("comment");
		    Field<NonNullGraphType<IdGraphType>>("productId");

        }
    }
}
