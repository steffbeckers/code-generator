using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.edit
{
    public partial class ModelEditComponentSpecTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelEditComponentSpecTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
