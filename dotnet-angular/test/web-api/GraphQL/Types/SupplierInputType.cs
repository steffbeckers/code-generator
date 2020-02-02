using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class SupplierInputType : InputObjectGraphType
    {
        public SupplierInputType()
        {
            Name = "supplierInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<StringGraphType>("phone");
        }
    }
}
