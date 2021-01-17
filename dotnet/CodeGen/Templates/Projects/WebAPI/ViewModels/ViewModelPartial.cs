using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.ViewModels
{
    public partial class ViewModel
    {
        private readonly CodeGenConfig _config;
        private readonly GenerateForEachModelData _data;
        private readonly CodeGenModel _model;

        public ViewModel(CodeGenConfig config, GenerateForEachModelData data, CodeGenModel model)
        {
            _config = config;
            _data = data;
            _model = model;
        }
    }
}
