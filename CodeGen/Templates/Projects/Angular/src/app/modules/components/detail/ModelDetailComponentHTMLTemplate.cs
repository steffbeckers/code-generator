using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.detail
{
    public partial class ModelDetailComponentHTMLTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelDetailComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
