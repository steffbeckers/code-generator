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
		    Field<StringGraphType>("code");
		    Field<IntGraphType>("quantity");
		    Field<FloatGraphType>("price");
        }
    }
}
