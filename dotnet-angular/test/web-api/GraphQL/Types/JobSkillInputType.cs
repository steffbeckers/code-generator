using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class JobSkillInputType : InputObjectGraphType
    {
        public JobSkillInputType()
        {
            Name = "jobSkillInput";
            Field<IdGraphType>("id");
		    Field<IntGraphType>("level");
		    Field<StringGraphType>("description");
		    Field<NonNullGraphType<IdGraphType>>("jobId");
		    Field<NonNullGraphType<IdGraphType>>("skillId");

        }
    }
}
