using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.ViewModels
{
    public partial class ViewModelTemplate
    {
        private readonly CodeGenConfig _config;
        private readonly CodeGenModel _model;

        public ViewModelTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
