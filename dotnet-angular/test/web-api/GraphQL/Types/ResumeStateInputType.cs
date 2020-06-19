using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class ResumeStateInputType : InputObjectGraphType
    {
        public ResumeStateInputType()
        {
            Name = "resumeStateInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<NonNullGraphType<StringGraphType>>("displayName");

        }
    }
}
