using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class DocumentTypeInputType : InputObjectGraphType
    {
        public DocumentTypeInputType()
        {
            Name = "documentTypeInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<NonNullGraphType<StringGraphType>>("displayName");
		    Field<StringGraphType>("test");

        }
    }
}
