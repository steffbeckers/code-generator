using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class TodoInputType : InputObjectGraphType
    {
        public TodoInputType()
        {
            Name = "todoInput";
		    Field<NonNullGraphType<StringGraphType>>("title");
		    Field<DateTimeGraphType>("dueDate");
        }
    }
}
