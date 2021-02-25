using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.shared.models
{
    public partial class ModelTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
