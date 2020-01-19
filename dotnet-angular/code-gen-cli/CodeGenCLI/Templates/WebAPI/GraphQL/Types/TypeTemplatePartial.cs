using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.GraphQL.Types
{
    public partial class TypeTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public TypeTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
