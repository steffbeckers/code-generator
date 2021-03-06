using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.create
{
    public partial class ModelCreateComponentCSSTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelCreateComponentCSSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
