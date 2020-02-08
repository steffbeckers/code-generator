using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI
{
    public partial class AppSettingsTemplate
    {
        private CodeGenConfig config;

        public AppSettingsTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
