using CodeGen.Models;

namespace CodeGen.Templates.Projects.WebAPI.CodeGenOutput.API.Requests
{
    public partial class GetByKeyTemplate : ITextTemplate
    {
        public CodeGenConfig _config;
        public CodeGenModel _model;

        public GetByKeyTemplate(CodeGenConfig config, CodeGenModel model)
        {
            _config = config;
            _model = model;
        }
    }
}
