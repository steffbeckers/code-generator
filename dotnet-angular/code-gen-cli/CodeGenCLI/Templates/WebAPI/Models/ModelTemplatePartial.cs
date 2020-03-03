using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.Models
{
    public partial class ModelTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public ModelTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
