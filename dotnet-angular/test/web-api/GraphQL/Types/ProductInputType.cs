using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class ProductInputType : InputObjectGraphType
    {
        public ProductInputType()
        {
            Name = "productInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<StringGraphType>("description");
		    Field<FloatGraphType>("price");

            // To create a link with Cart directly on create of Product.
            //Field<IdGraphType>("cartId");
            //Field<NonNullGraphType<IntGraphType>>("cartQuantity");
            //Field<NonNullGraphType<FloatGraphType>>("cartPrice");
        }
    }
}
