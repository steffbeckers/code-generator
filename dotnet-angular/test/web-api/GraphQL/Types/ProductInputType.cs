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
		    Field<BooleanGraphType>("active");

            // To create a link with Supplier directly on create of Product.
            //Field<IdGraphType>("supplierId");
            //Field<StringGraphType>("supplierComment");
        }
    }
}
