using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class ProductSupplierInputType : InputObjectGraphType
    {
        public ProductSupplierInputType()
        {
            Name = "productSupplierInput";
            Field<IdGraphType>("id");
		    Field<StringGraphType>("comment");
		    Field<NonNullGraphType<IdGraphType>>("productId");
		    Field<NonNullGraphType<IdGraphType>>("supplierId");
        }
    }
}
