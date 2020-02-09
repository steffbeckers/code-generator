using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.DAL.Repositories
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
