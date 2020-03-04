using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class RelationTypeInputType : InputObjectGraphType
    {
        public RelationTypeInputType()
        {
            Name = "relationTypeInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<IdGraphType>>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");

        }
    }
}
