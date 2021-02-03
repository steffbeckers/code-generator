using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.Models
{
    public partial class ModelTemplate : ITextTemplate
    {
        private readonly CodeGenConfig _config;
        private readonly CodeGenModel _model;

        public ModelTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
