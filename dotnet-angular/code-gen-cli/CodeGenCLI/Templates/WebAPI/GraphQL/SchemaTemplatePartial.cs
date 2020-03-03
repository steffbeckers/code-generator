using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.GraphQL
{
    public partial class SchemaTemplate
    {
        private CodeGenConfig config;

        public SchemaTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
