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

            // To create a link with Skill directly on create of Resume.
            //Field<IdGraphType>("skillId");
            //Field<NonNullGraphType<IntGraphType>>("skillRating");
            //Field<StringGraphType>("skillDescription");
        }
    }
}
