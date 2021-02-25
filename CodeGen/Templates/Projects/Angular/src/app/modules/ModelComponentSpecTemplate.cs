using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules
{
    public partial class ModelComponentSpecTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelComponentSpecTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
