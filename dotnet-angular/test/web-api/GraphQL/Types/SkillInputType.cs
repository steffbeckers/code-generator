using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class SkillInputType : InputObjectGraphType
    {
        public SkillInputType()
        {
            Name = "skillInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<StringGraphType>("description");

            // To create a link with Resume directly on create of Skill.
            //Field<IdGraphType>("resumeId");
            //Field<IntGraphType>("resumeLevel");
            //Field<StringGraphType>("resumeDescription");
            // To create a link with Job directly on create of Skill.
            //Field<IdGraphType>("jobId");
            //Field<IntGraphType>("jobLevel");
            //Field<StringGraphType>("jobDescription");
        }
    }
}
