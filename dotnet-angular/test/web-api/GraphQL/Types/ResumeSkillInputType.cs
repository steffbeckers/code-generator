using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class ResumeSkillInputType : InputObjectGraphType
    {
        public ResumeSkillInputType()
        {
            Name = "resumeSkillInput";
            Field<IdGraphType>("id");
		    Field<IntGraphType>("level");
		    Field<StringGraphType>("description");
		    Field<NonNullGraphType<IdGraphType>>("resumeId");
		    Field<NonNullGraphType<IdGraphType>>("skillId");

        }
    }
}
