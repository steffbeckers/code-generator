using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.Services
{
    public partial class EmailServiceTemplate
    {
        private CodeGenConfig config;

        public EmailServiceTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
