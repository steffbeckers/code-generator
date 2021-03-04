using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.edit
{
    public partial class ModelEditComponentCSSTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelEditComponentCSSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
