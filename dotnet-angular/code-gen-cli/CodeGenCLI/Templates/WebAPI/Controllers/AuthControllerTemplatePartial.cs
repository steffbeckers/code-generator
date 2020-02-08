using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.Controllers
{
    public partial class AuthControllerTemplate
    {
        private CodeGenConfig config;

        public AuthControllerTemplate(CodeGenConfig config)
        {
            this.config = config;
        }
    }
}
