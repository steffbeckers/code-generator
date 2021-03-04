using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.detail
{
    public partial class ModelDetailComponentSpecTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelDetailComponentSpecTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
