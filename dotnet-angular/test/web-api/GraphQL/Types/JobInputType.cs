using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class JobInputType : InputObjectGraphType
    {
        public JobInputType()
        {
            Name = "jobInput";
            Field<IdGraphType>("id");
		    Field<StringGraphType>("title");
		    Field<StringGraphType>("description");
		    Field<NonNullGraphType<IdGraphType>>("jobStateId");

            // To create a link with Skill directly on create of Job.
            //Field<IdGraphType>("skillId");
            //Field<IntGraphType>("skillRating");
            //Field<StringGraphType>("skillDescription");
        }
    }
}
