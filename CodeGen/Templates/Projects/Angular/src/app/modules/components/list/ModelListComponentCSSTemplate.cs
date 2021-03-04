using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.list
{
    public partial class ModelListComponentCSSTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelListComponentCSSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
