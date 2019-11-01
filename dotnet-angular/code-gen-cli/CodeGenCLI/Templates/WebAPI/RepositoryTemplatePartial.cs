using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI
{
    public partial class RepositoryTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public RepositoryTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
