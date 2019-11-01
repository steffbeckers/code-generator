using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI
{
    public partial class StartupTemplate
    {
        private CodeGenConfig config;

        public StartupTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
