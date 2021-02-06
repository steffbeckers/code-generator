using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPICQRS.CodeGenOutput.API.ViewModels
{
    public partial class ViewModelTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ViewModelTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
