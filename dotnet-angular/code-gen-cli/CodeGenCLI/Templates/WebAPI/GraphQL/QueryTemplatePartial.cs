using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.GraphQL
{
    public partial class QueryTemplate
    {
        private CodeGenConfig config;

        public QueryTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
