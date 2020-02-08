using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.Controllers
{
    public partial class ControllerTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public ControllerTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
