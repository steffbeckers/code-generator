using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPICQRS.CodeGenOutput.API.Models
{
    public partial class ModelTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public ModelTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
