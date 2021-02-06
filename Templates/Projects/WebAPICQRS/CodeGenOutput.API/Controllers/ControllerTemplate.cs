using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPICQRS.CodeGenOutput.API.Controllers
{
    public partial class ControllerTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ControllerTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
