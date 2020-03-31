using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class ResumeInputType : InputObjectGraphType
    {
        public ResumeInputType()
        {
            Name = "resumeInput";
            Field<IdGraphType>("id");
		    Field<StringGraphType>("jobTitle");
		    Field<StringGraphType>("description");
		    Field<NonNullGraphType<IdGraphType>>("resumeStateId");

            // To create a link with Document directly on create of Resume.
            //Field<IdGraphType>("documentId");
            // To create a link with Skill directly on create of Resume.
            //Field<IdGraphType>("skillId");
            //Field<IntGraphType>("skillLevel");
            //Field<StringGraphType>("skillDescription");
        }
    }
}
