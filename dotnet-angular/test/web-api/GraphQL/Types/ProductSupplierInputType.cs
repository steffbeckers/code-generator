using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class ProductSupplierInputType : InputObjectGraphType
    {
        public ProductSupplierInputType()
        {
            Name = "productSupplierInput";
		    Field<NonNullGraphType<StringGraphType>>("comment");
        }
    }
}
