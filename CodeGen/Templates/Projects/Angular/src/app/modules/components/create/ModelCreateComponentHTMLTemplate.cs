using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.create
{
    public partial class ModelCreateComponentHTMLTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelCreateComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
