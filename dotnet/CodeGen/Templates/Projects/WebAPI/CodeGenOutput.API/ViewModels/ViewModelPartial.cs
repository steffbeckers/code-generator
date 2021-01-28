using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.ViewModels
{
    public partial class ViewModel
    {
        private readonly CodeGenConfig _config;
        private readonly CodeGenModel _model;

        public ViewModel(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
