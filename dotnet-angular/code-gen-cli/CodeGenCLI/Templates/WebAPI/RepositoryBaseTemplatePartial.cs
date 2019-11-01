using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI
{
    public partial class RepositoryBaseTemplate
    {
        private CodeGenConfig config;

        public RepositoryBaseTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
