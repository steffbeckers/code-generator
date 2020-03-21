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
            //Field<IntGraphType>("resumeRating");
            //Field<StringGraphType>("resumeDescription");
        }
    }
}
