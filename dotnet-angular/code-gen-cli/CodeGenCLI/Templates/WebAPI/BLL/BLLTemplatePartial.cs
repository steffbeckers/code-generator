using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.BLL
{
    public partial class BLLTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public BLLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
