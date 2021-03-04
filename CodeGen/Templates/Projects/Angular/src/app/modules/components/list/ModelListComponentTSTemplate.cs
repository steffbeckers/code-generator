using CodeGen.Models;

namespace CodeGen.Templates.Projects.Angular.src.app.modules.components.list
{
    public partial class ModelListComponentTSTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelListComponentTSTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
