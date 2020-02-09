using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.GraphQL.Types
{
    public partial class UserTypeTemplate
    {
        private CodeGenConfig config;

        public UserTypeTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
