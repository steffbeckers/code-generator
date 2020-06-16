using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class DocumentResumeInputType : InputObjectGraphType
    {
        public DocumentResumeInputType()
        {
            Name = "documentResumeInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<IdGraphType>>("documentId");
		    Field<NonNullGraphType<IdGraphType>>("resumeId");

        }
    }
}
