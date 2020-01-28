using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class SupplierInputType : InputObjectGraphType
    {
        public SupplierInputType()
        {
            Name = "supplierInput";
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<StringGraphType>("phone");
        }
    }
}
