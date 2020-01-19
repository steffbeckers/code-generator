using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class NoteInputType : InputObjectGraphType
    {
        public NoteInputType()
        {
            Name = "noteInput";
		    Field<NonNullGraphType<StringGraphType>>("title");
		    Field<StringGraphType>("body");
        }
    }
}
