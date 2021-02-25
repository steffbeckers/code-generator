using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules
{
    public partial class ModelComponentHTMLTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelComponentHTMLTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
