using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.GraphQL.Types
{
    public partial class InputTypeTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public InputTypeTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
