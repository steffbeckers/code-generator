using CodeGenCLI.CodeGenClasses;

namespace CodeGenCLI.Templates.WebAPI.ViewModels
{
    public partial class ViewModelTemplate
    {
        private CodeGenConfig config;
        private CodeGenModel model;

        public ViewModelTemplate(CodeGenConfig config, CodeGenModel model)
        {
            this.config = config;
            this.model = model;
        }
    }
}
