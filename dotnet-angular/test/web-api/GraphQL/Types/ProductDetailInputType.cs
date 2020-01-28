using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class ProductDetailInputType : InputObjectGraphType
    {
        public ProductDetailInputType()
        {
            Name = "productDetailInput";
		    Field<NonNullGraphType<StringGraphType>>("comment");
        }
    }
}
