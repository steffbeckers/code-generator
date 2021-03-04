using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.list
{
    public partial class ModelListComponentHTMLTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelListComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
