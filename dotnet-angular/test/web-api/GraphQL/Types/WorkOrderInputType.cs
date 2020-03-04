using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class WorkOrderInputType : InputObjectGraphType
    {
        public WorkOrderInputType()
        {
            Name = "workOrderInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<DateTimeGraphType>>("date");
		    Field<NonNullGraphType<IdGraphType>>("accountId");

        }
    }
}
