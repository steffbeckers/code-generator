using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.edit
{
    public partial class ModelEditComponentHTMLTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelEditComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
