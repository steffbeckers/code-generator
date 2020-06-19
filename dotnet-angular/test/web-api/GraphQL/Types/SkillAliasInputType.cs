using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class SkillAliasInputType : InputObjectGraphType
    {
        public SkillAliasInputType()
        {
            Name = "skillAliasInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<StringGraphType>("description");
		    Field<NonNullGraphType<IdGraphType>>("skillId");

        }
    }
}
