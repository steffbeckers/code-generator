using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class DocumentInputType : InputObjectGraphType
    {
        public DocumentInputType()
        {
            Name = "documentInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<StringGraphType>("displayName");
		    Field<StringGraphType>("description");
		    Field<StringGraphType>("path");
		    Field<StringGraphType>("uRL");
		    Field<StringGraphType>("mimeType");
		    Field<NonNullGraphType<IdGraphType>>("resumeStateId");

            // To create a link with Skill directly on create of Document.
            //Field<IdGraphType>("skillId");
            //Field<IntGraphType>("skillLevel");
            //Field<StringGraphType>("skillDescription");
        }
    }
}