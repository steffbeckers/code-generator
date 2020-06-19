using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class JobStateInputType : InputObjectGraphType
    {
        public JobStateInputType()
        {
            Name = "jobStateInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("name");
		    Field<NonNullGraphType<StringGraphType>>("displayName");

        }
    }
}
