using GraphQL.Types;

namespace RJM.API.GraphQL.Types
{
    public class SettingInputType : InputObjectGraphType
    {
        public SettingInputType()
        {
            Name = "settingInput";
            Field<IdGraphType>("id");
		    Field<NonNullGraphType<StringGraphType>>("key");
		    Field<StringGraphType>("value");

        }
    }
}
