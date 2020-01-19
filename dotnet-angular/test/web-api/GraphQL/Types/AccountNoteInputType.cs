using GraphQL.Types;

namespace Test.API.GraphQL.Types
{
    public class AccountNoteInputType : InputObjectGraphType
    {
        public AccountNoteInputType()
        {
            Name = "accountNoteInput";
        }
    }
}
